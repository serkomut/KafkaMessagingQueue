## Messages
CQRS mimaride bütün end-point mesajlarının toplandığı katmandır. (Bkz: [Messaging Patterns](https://www.enterpriseintegrationpatterns.com/patterns/messaging/index.html))

Bu katmanda Command'lar için `CommandResponse<T>` adında bir sınıf kullanılmıştır. Amaç microservice mimarilerde `Aggregator` pattern anlayışından yola çıkarak; birden fazla yanıt ihtiyacı duyulduğunda kullanabilecek, Open/Closed prensipli bir **messaging router** var etmektir. (Bkz: [Aggregator](https://www.enterpriseintegrationpatterns.com/Aggregator.html))