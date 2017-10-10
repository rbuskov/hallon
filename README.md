# Hallon 

Hallon is an implementation of the HAL standard (JSON Hypertext Application Language) for .NET Web API 2. Draft specification for HAL is available at https://tools.ietf.org/html/draft-kelly-json-hal-08.

## Design goals

- Do not pollute domain models
- Convention over configuration (should just work for simple scenarios)
- Choice between fluent API and attributes for configuration
- Allow arbitrary links and embedded resources (the domain model may not be complete, so don't limit the user to stuff that can be found via reflection)
- Allow for links to be made available dynamically, based on application state

## Basic Use
To use Hallon, simply configure the supplied HAL media formatter on start up:
```
config.formatters.Add(new HalMediaFormatter();
```
This will cause all controllers to respond to requests for "application/hal+json" with (basic) HAL complient JSON output, as shown below.

### Examples
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
Note that "self" links are added automatically while other links must be configured by the developer.

## Fluent Configuration

Resources can be enriched with additional properties by passing a HAL configuration object to the media formatter during start up:
```
class HalConfig : FluentHalConfiguration
{
   ...
}

...

config.formatters.Add(new HalMediaFormatter(new HalConfig());
```

### Examples
Add link to an object resource (in HalConfig):

```
AddResource<Order>()
   .WithLink("customer", order => $"customers/{order.Customer.Id}");
```

Add link conditionally:

```
AddResource<Order>()
   .WithLink("customer", order => $"customers/{order.Customer.Id}", order => order != null);
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

## Links

https://docs.microsoft.com/en-us/aspnet/web-api/overview/getting-started-with-aspnet-web-api/action-results
http://restful-api-design.readthedocs.io
