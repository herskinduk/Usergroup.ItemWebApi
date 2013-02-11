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

Note: For authentication the username is looked up in the website domain.

Note: ItemConsole uses version 4.5 of NewtonSoft.Json.dll. This may not be the same version as the one used in Sitecore... so don't copy it there.
RequestThrottle
==
An example of a pipeline processor that will throttle the API requests per ip address. The example uses a moving average strategy.

Use the following configuration include to add throttling to API request pipeline:

	<?xml version="1.0"?>
	<configuration xmlns:patch="http://www.sitecore.net/xmlconfig/">
	  <sitecore>
		<pipelines>
		  <itemWebApiRequest>
			<processor patch:before="*[1]" type="ItemWebApiExtension.RequestThrottle, ItemWebApiExtension" />
		  </itemWebApiRequest>
		</pipelines>
	  </sitecore>
	</configuration>

