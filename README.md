# Hallon 

Hallon is an implementation of the HAL standard (JSON Hypertext Application Language) for .NET Web API 2. The latest draft specification for HAL is available at https://tools.ietf.org/html/draft-kelly-json-hal-08.

Hallon means "raspberry" in Swedish.

## Design goals

- Do not pollute domain models
- Convention over configuration (should just work for simple scenarios)
- HAL responses can be configured i 3 ways
-- Implementing Hallon.IResource (abstract base class provided)  
-- Adding attributes to classes and members (both POCO and/or Resource objects) 
-- Via a fluent API that is detached from the domian model (both POCO and/or Resource objects) 
- Allow arbitrary links and embedded resources (don't limit the developer to stuff that can be found via reflection)
- Allow for links to be made available dynamically, based on application state

## Basic Use
To use Hallon, simply add the supplied HAL media formatter on start up:
```
config.Formatters.Add(new HalMediaTypeFormatter());
```
This will cause all controllers in the application to respond to requests for "application/hal+json" with basic HAL complient JSON output, as shown below.

#### Examples
Single object resource:
```
GET /orders/523 HTTP/1.1
   Host: example.org
   Accept: application/hal+json

   HTTP/1.1 200 OK
   Content-Type: application/hal+json

   {
     "_links": {
       "self": { "href": "/orders/523" },
     },
     "currency": "USD",
     "status": "shipped",
     "total": 10.20
   }
```

Collection resource:
```
GET /orders HTTP/1.1
Host: example.org
Accept: application/hal+json

HTTP/1.1 200 OK
Content-Type: application/hal+json

{
  "_links": {
    "self": { "href": "/orders" },
  },
  "_embedded": {
    "orders": [{
        "_links": {
          "self": { "href": "/orders/123" },
        },
        "total": 30.00,
        "currency": "USD",
        "status": "shipped",
      },{
        "_links": {
          "self": { "href": "/orders/124" },
        },
        "total": 20.00,
        "currency": "USD",
        "status": "processing"
    }]
  },
  "currentlyProcessing": 14,
  "shippedToday": 20
}
```
The default behaviour is to include all properties that have primitive values in the HAL output. Note that "self" links are added automatically while other links must be configured by the developer.

## Implementing IResource

...

## Attributes

...

## Fluent Configuration

Resources can be customized by passing a HAL configuration object to the media formatter during start up:
```
class HalConfig : FluentHalConfiguration
{
   ...
}

...

config.formatters.Add(new HalMediaFormatter(new HalConfig());
```

#### Examples

Add static link to a resource (in HalConfig):

```
AddResource<Order>()
   .WithLink("customers", "/customers");
```

Add a dynamic link to resource:

```
AddResource<Order>()
   .WithLink("customer", order => $"/customers/{order.Customer.Id}");
```

Add a link conditionally:

```
AddResource<Order>()
   .WithLink("customer", order => $"/customers/{order.Customer.Id}", order => order.Customer != null);
```

Configure a collection resource:

```
AddCollectionResource<Order>()
   .WithProperty("grandTotal", orders => orders.Sum(o => o.Total));
   .AddEmbeddedItems()
      .WithLink("customer", order => $"customers/{order.Customer.Id}", order => order != null);
      .WithProperty("id", order => order.Id)
      .WithProperty("date", order => order.Date);
      .WithProperty("total", order => order.Total);
```
Specifying one or more properties for a resource overrides the default behaviour of including all properties in the output. Only primitive property values can be configured.

## Links

https://docs.microsoft.com/en-us/aspnet/web-api/overview/getting-started-with-aspnet-web-api/action-results
http://restful-api-design.readthedocs.io
