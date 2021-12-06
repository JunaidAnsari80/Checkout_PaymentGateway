
# Checkout_PaymentGateway


## The task

The product requirements for this initial phase are the following: 
1. A merchant should be able to process a payment through the payment gateway and receive either a successful or unsuccessful response. 
2. A merchant should be able to retrieve the details of a previously made payment. 


# Implementation
## Dependencies

 - Mediatr
 - FluentValidation
 - NewtonSoft
 - Swashbuckle
 - EF Inmemory

## How to run.
Hit F5 to run.
Solution is setup to run 2 projects. GatewayAPI runs in debug mode where Simulator run in non debug mode.

### Make a transaction
To make a successful transaction pass idempotentID as "1A". For rejected transaction pass idempotentID as "1B".

**Request model**
{
  "amount": 10,
  "merchantID": 10,
  "idempotentID": "1A",
  "cardNumber": "5295650000000000",
  "cvv": "000",
  "cardholderName": "jamesbond",
  "expiryMonth": 10,
  "expiryYear": 2020,
  "cardCurrency": "GBP",
  "firstLineOfAddress": "string",
  "postCode": "string"
}

## Assumptions.

 - Merchant authorization is implemented.
 - Merchant manage idempotency.
 - 

## Cloud technologies?
2 Options.
 - Server-less environment; where having AWS lambda functions acting as microservices with API-Gateway at front, Global DynamoDB as persistent store. Advantage of using server-less infrastructure is they are managed services. Easy to scale on demand and they are cost effective as we are charged for what we use but with very high usage will result in fat monthly bill.
 - Host microservice as containerized application, with mix of AWS managed services (DynamoDB). It is cost effective and very cheap but require high maintenance.

## Improvements.

 - Use notification/events to update payment status from payment service asynchronously.
 - Implement logging.
