Sitecore User Group: Sitecore Item Web API
===

This is sample code used for the Sitecore User Group presentation on Sitecore Item Web API.

There are projects in the solution:

ItemConsole
==
This is a Console application with examples of how you interact with the Sitecore Item Web API programatically from C#. There are examples of how to:

  - Do an authenticated request with clear text credentials
  - Do an authenticated request with RSA encrypted credentials
  - Update a field on an item
  - Create/delete items

RequestThrottle
==
An example of a pipeline processor that will throttle the API requests per ip address. The example uses a moving average strategy.

Use the following configuration include to add throttling to API request pipeline:

	<?xml version="1.0"?>
	<configuration xmlns:patch="http://www.sitecore.net/xmlconfig/">
	  <sitecore>
		<pipelines>
		  <itemWebApiRequest>
			<processor type="ItemWebApiExtension.RequestThrottle, ItemWebApiExtension" />
		  </itemWebApiRequest>
		</pipelines>
	  </sitecore>
	</configuration>

