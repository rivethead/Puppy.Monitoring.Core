# Skinny.Monitoring #

A monitoring library heavily influenced by [http://wolfpack.codeplex.com/](http://wolfpack.codeplex.com/ "Wolfpack") to monitor

- Successes / failures in the application
- Duration of external system calls
- Requests and responses between external system calls

The library is split into three parts

- Reporting
- Measuring
- Tracking

## Reporting ##

The idea behind the reporting is to allow a developer to record events during the execution of the application. The simplest two events is the `SuccessEvent` and `FailureEvent`, but a developer can add their own event implementations by deriving a new event implementation from the Event class.

The report interface give you three options, report a success event, a failure event or a custom event.

    Report 
  		.Success() // success! 
		.Publish(); // publish the event using the parameters supplied 

	Report
		.Failure() // failure
		.Publish(); // publish the event

	Report
		.CustomEvent()
		.Event(new MyCustomEvent()) // MyCustomEvent implements IEvent
		.Publish();


## Measurement ##

The measurement part of the library is put in place to measure the time a call takes to execute and report based on the outcome of the call. Normally this is used when calling external services (e.g. web service, an api) but you can measure any call you fancy. The measurement functionality will set the `Timings.Took` value on the published event to the time (in milliseconds) the call took to complete.

    Measure
		.This<TResponse>(() => {
								// execute call to be measured and return an instance of TResponse
								})
		.OnSuccess(() => Report.Success()) // if the call completes a success event is published
		.OnFailure(() => Report.Failure()) // if the fails a failure event is published
		.Gauge() // do it!

## Tracking ##

The tracking element of the library builds on the Reporting and Measurement functionality to track communication between the local application and an external service. The Tracking functionality basically takes a call, the request used in the call and the response of the call and writes the request and response to a persistent data store. Once the call completed (or not) an event is published.

    Track<ExternalCallResponse>
    	.Call(() => call_to_external_service()) // the call to an external service
    .Write()
        .With(tracking_writer) // write the request and response to a persistent store using this implementation of IWriteTracking
        .UsingAsIdentifier(() => identifier) // what is the unique identifier for the call
        .TheRequest(request) // the request sent to the external service
        .TheResponse(response => response.ToString()) // the serialised response from the external service
    .Report()
        .Success(Report.Success) // if the call completes publish this event
		.Failure(Report.Failure) // if the call fails publish this event
    .Go();

## Publishing ##
Now the more interesting part (I hope!). Using the fluent interface to define **what event** should be published **when** is pretty straight forward, but somewhere you need to define **how** the event will be published. 

### Pipelines ###

The publishing is done by using a publishing pipeline made up of pipelets. Each pipelet takes an event (of type `IEvent`) performs an action and then has the option of returning another list of events to be put back into the pipeline.

There are 5 implementations (currently) of the `IPipeline` interface. 

- `NullPipeline` surprising does nothing to the event
- `LinearPipeline` takes the original event and flows it to each pipelet in the pipeline, but it **does not** add the returned events from the pipelets into the pipeline.
- `QueuedEventsPipeline` takes the original event and adds the event to an internal queue, the first event in the queue (the original event at this point in time) is then flowed to each pipelet. Each event returned from a pipelet is added to the internal queue. The pipeline will continue flowing events from the internal queue to each pipelet until the internal queue is empty. I like this one.
- `CompleteAndReflowPipeline` will flow the original event to each pipelet. The events returned from each pipelet is added to a internal list. Once the original event is published to each pipelet, the events in the internal list is flowed to each pipelet. The process repeats itself recursively so be aware of circular pipelines.
- `ReflowOnEventPipeline` will flow the original event to each pipelet. If a pipelet returns events, the returned events will be flowed into the pipeline **immediately**. Once the new events are flowed, the original event is flowed to the next pipelet. Again beware of circular pipelines.

### Pipelets ###
Yes I realise it is not a word. A pipelet is a part in a pipeline and performs an action based on the event being flowed to the pipelet.

What the pipelet do is up to you, but there are a couple of default pipelets implemented currently. But first how to you implement your own pipelet?

Simple. Inherit from the `BasePipelet` base class and off you go.

    public class MyPipelet : BasePipelet
    {
        private readonly IEventSpecification filter; // the filter to apply to determine whether the pipelet should be invoked

        public MyPipelet(IEventSpecification filter)            
        {
            this.filter = filter;
        }

        protected override bool FilterEvent(IEvent @event)
        {
            return filter.SatisfiedBy(@event);
        }

        protected override IEnumerable<IEvent> Accept(IEvent @event)
        {

            // do magic here

            return ListOfEvents.Create(new NoopEvent()); // return a list of events if applicable
        }
    }

Currently there are default pipelets available from Puppy.

### Event filters ###
The purpose of event filters is to filter out events in which a pipelet is **not** interested. An example would be; if you have a pipelet that counts the number of `FailureEvent` events, the pipelet is only interested in events of type `FailureEvent`. In order to filter the other events implement the `IEventSpecification` interface and inject the filter into the pipelet. The `BasePipelet` implementation will filter out any events **not** satisfying your filter but invoke your pipelet for all the events **satisfying** your filter. Again there are a couple of filter implementations available already.

To implement the filter mentioned above the code will look as follows:

    public class FailureEventSpecification : IEventSpecification
    {
        private readonly IEventSpecification successor = new NullEventSpecification();

        public FailureEventSpecification()
        {
        }

        public FailureEventSpecification(IEventSpecification successor)
        {
            this.successor = successor;
        }

        public bool SatisfiedBy(IEvent @event)
        {
            return @event.GetType() == typeof (FailureEvent) && successor.SatisfiedBy(@event);
        }
    }

Notice the constructor taking another instance of `IEventSpecification`. This allows you to build chains of filters and mix and match filters to form complex (whilst easy to understand) filters.

### Publisher ###
So now we have pipelines built up from pipelets. Pipelets using event filters to be invoked for the applicable events. We use the Report, Measure and Track fluent interfaces to define which events should be published when. Grand. The next question is how do we link all of this together? The glue to bring all the components together called the `Publisher`.

The `Publisher` publishes events to a pipeline. Which pipeline is defined by an adapter (e.g. `ManualPipelineAdapter` or any implementation of `IPipelineAdapter`). To connect the `Publisher` with an adapter you call the following piece of code

    Publisher.Use(new ManualPipelineAdapter(), new PublishingContext("MY_SYSTEM", "MY_MODULE"));

This line above indicates to the `Publisher` how to get the pipeline when an event is published. 

You only need to do this once in your application (e.g. Global.asax.cs)

#### Pipeline adapters ####
The purpose of a pipeline adapter is to take an event from the `Publisher` and pass the event to the appropriate pipeline. The appropriate pipeline will then flow the event to each pipelet. 

The `ManualPipelineAdapter` uses manual registration of pipelines. But pipelines can be resolved from an IoC container or any other source.


### Publisher ###
So now we have pipelines built up from pipelets. Pipelets using event filters to be invoked for the applicable events. We use the Report, Measure and Track fluent interfaces to define which events should be published when. Grand. The next question is how do we link all of this together? The glue to bring all the components together called the `Publisher`.

The `Publisher` publishes events to a pipeline. Which pipeline is defined by an adapter (e.g. `ManualPipelineAdapter` or any implementation of `IPipelineAdapter`). To connect the `Publisher` with an adapter you call the following piece of code

    Publisher.Use(new ManualPipelineAdapter(), new PublishingContext("MY_SYSTEM", "MY_MODULE"));

This line above indicates to the `Publisher` how to get the pipeline when an event is published. 

You only need to do this once in your application (e.g. Global.asax.cs)

#### Pipeline adapters ####
The purpose of a pipeline adapter is to take an event from the `Publisher` and pass the event to the appropriate pipeline. The appropriate pipeline will then flow the event to each pipelet. 

The `ManualPipelineAdapter` uses manual registration of pipelines. But pipelines can be resolved from an IoC container or any other source.
