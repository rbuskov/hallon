# Hallon - HAL Framework for .NET Web API 2

Implementation of HAL (JSON Hypertext Application Language) according to the draft specification: https://tools.ietf.org/html/draft-kelly-json-hal-08

## Design goals

- Do not pollute domain models
- Convention over configuration (should just work for simple scenarios)
- Choice between fluent API and attributes for configuration
- Allow arbitrary links and embedded resources (the domain model may not be complete, so don't limit the user to stuff that can be found via reflection)
- Allow for links to be made available dynamically, based on application state

## Examples
Single resource:
```
GET /orders/523 HTTP/1.1
   Host: example.org
   Accept: application/hal+json

   HTTP/1.1 200 OK
   Content-Type: application/hal+json

   {
     "_links": {
       "self": { "href": "/orders/523" },
       "warehouse": { "href": "/warehouse/56" },
       "invoice": { "href": "/invoices/873" }
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
    "next": { "href": "/orders?page=2" },
    "find": { "href": "/orders{?id}", "templated": true }
  },
  "_embedded": {
    "orders": [{
        "_links": {
          "self": { "href": "/orders/123" },
          "basket": { "href": "/baskets/98712" },
          "customer": { "href": "/customers/7809" }
        },
        "total": 30.00,
        "currency": "USD",
        "status": "shipped",
      },{
        "_links": {
          "self": { "href": "/orders/124" },
          "basket": { "href": "/baskets/97213" },
          "customer": { "href": "/customers/12369" }
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

## Solution Design

- Create a HalMediaTypeFormatter to handle "application/hal+json"
- Use the standard JsonMediaTypeFormatter internally, so the new formatter can handle any type that the standard formatter can handle
- Once configured, all controllers that return domain objects WILL respond with the hal+json media type (naive but functional responses)
- Behaviours can be configured further with Fluent API and/or attributes
- Links (to actions) may or may not be available depending on the state of the resource
- Decorate comntrollers with attributes, not domain models (makes testing hard?)
- The formatter should be testable
- How about a HAL client??!

## Actions

```
 {
     "_links": {
       "self": { "href": "/orders/523" },
       "cancel": { "href": "/orders/523/cancel", "action": true }
```
Sometimes, it is required to expose an operation in the API that inherently is non RESTful. One example of such an operation is where you want to introduce a state change for a resource, but there are multiple ways in which the same final state can be achieved, and those ways actually differ in a significant but non-observable side-effect. Some may say such transitions are bad API design, but not having to model all state can greatly simplify an API. A great example of this is the difference between a “power off” and a “shutdown” of a virtual machine. Both will lead to a vm resource in the “DOWN” state. However, these operations are quite different.

As a solution to such non-RESTful operations, an “actions” sub-collection can be used on a resource. Actions are basically RPC-like messages to a resource to perform a certain operation. The “actions” sub-collection can be seen as a command queue to which new action can be POSTed, that are then executed by the API. Each action resource that is POSTed, should have a “type” attribute that indicates the type of action to be performed, and can have arbitrary other attributes that parameterize the operation.

It should be noted that actions should only be used as an exception, when there’s a good reason that an operation cannot be mapped to one of the standard RESTful methods. If an API has too many actions, then that’s an indication that either it was designed with an RPC viewpoint rather than using RESTful principles, or that the API in question is naturally a better fit for an RPC type model.

## Links


https://docs.microsoft.com/en-us/aspnet/web-api/overview/getting-started-with-aspnet-web-api/action-results
http://restful-api-design.readthedocs.io
