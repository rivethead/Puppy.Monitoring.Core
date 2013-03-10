@nugets = [
	{
		:package_id => 'Puppy.Monitoring',
		:description => 'Puppy.Monitoring',
		:authors => 'rivethead_',
		:base_folder => 'Puppy.Monitoring/',
		:files => [
			['Puppy.Monitoring.dll', 'lib\net45'],
			['Puppy.Monitoring.pdb', 'lib\net45'],
		],
		:dependencies => [
			['Common.Logging', '(2.1.1,2.12)']
		]
	},
	{
		:package_id => 'Puppy.Monitoring.Contrib',
		:description => 'Puppy.Monitoring.Contrib',
		:authors => 'rivethead_',
		:base_folder => 'Puppy.Monitoring.Contrib/',
		:files => [
			['Puppy.Monitoring.Contrib.dll', 'lib\net45'],
			['Puppy.Monitoring.Contrib.pdb', 'lib\net45']
		],
		:dependencies => [
			['Puppy.Monitoring', '']
		]
	}
]