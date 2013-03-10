@nugets = [
	{
		:package_id => 'Puppy.Monitoring',
		:description => 'Puppy.Monitoring core components. See the readme on https://github.com/rivethead/Puppy.Monitoring.Core for more information',
		:authors => 'rivethead_',
		:base_folder => 'Puppy.Monitoring/',
		:web_site => 'https://github.com/rivethead/Puppy.Monitoring.Core',
		:files => [
			['Puppy.Monitoring.dll', 'lib\net45'],
			['Puppy.Monitoring.pdb', 'lib\net45'],
		],
		:dependencies => [
			['Common.Logging', '[2.1.1,2.12]']
		]
	},
	{
		:package_id => 'Puppy.Monitoring.Contrib',
		:description => 'Contributions to the core which fall outside the scope of the core component',
		:authors => 'rivethead_',
		:web_site => 'https://github.com/rivethead/Puppy.Monitoring.Core',
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