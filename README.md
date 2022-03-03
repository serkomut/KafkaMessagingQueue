# KafkaMessagingQueue
- **[İster ve kapsam için tıklayın.](docs/README.MD)**
- **[Domain](docs/DOMAIN.MD)**
- **[Persistence](docs/PERSISTENCE.MD)**
- **[Messages](docs/MESSAGES.MD)**
- **[Queries ve Commands](docs/QUERIES_COMMANDS.MD)**
- **[Code Coverage Result](docs/CODE_COVERAGE.MD)**
___
## Gereksinimler
**1. Kafkayı kurun ve çalıştırın.** Versiyon 2.0.1
- [Kafka'yı indirin.](https://kafka.apache.org/downloads)
- İndirdiğiniz dosyayı bilgisayarınız diskinde uygun gördüğünüz bir klasöre taşıyın. Aşağıdaki gibi;
- ![alt text](docs/kafka_folder.png "Kafka")
- Kafka için bilgisayarınızda `Environment Variables` ayarlayın.
- ![alt text](docs/kafka_variables.png "kafka variables")
- Çalıştırmak için konsola`kafka-server-start.bat %KAFKA_HOME%\config\server.properties` yazın ve enter tuşuna basın.

**2. Zookeper kurun ve çalıştırın.** Versiyon 3.4.13
- [Zookeper'ı indirin.](https://zookeeper.apache.org/releases.html)
- İndirdiğiniz dosyayı bilgisayarınız diskinde uygun gördüğünüz bir klasöre taşıyın. Aşağıdaki gibi;
- ![alt text](docs/zookeeper_folder.png "Zookeeper")
- Zookeepr için bilgisayarınızda `Environment Variables` ayarlayın.
- ![alt text](docs/zookeeper_variables.png "zookeeper variables")
- Çalıştırmak için konsala`zkserver` yazın ve enter'a basın.

**3. RabbitMQ kurun.**

[https://www.rabbitmq.com/](https://www.rabbitmq.com/)

veya

`choco install rabbitmq`

**4. PostgreSQL** Versiyon 14


#### Kullanılan Topic'ler

`kafka-console-consumer.bat --bootstrap-server localhost:9092 --topic PreparingEvent`

`kafka-console-consumer.bat --bootstrap-server localhost:9092 --topic CompletedEvent`

**PreparingEvent**

Bu event, rapor talep edildiğinde isteği kuyruğa attığını, dinleyen consumer'lar için `Preparing` durumunda event fırlatır. Aynı zamanda rapor talebini database'e `Preparing` satatus'unda kaydeder.

**CompletedEvent**

Bu event ise kuyruğun tamamladığı rapor taleplerini `Copleted` status'üne çekip dinleyen consumer'lar için bir event fırlatır. Aynı zamanda `Preparing` olarak kaydedilmiş status'ü `Completed` olarak günceller.

#### Database Migration

Eğer uygulamaların `appsettings.json` dosyasında bulunan aşağıdaki alanın değerine `InMemory` yazılırsa uygulama InMemory database olarak çalışacaktır.
```xml
"DatabaseConfigurationType": "", // InMemory
```

**KafkaMessagingQueue.Api** projesinin database'ni migration yapmak için;

`$ ..\KafkaMessagingQueue.Persistence> dotnet ef migrations add Guide-1.0.1 -s ..\KafkaMessagingQueue.Api\`

Var olan migration'u database'e update etmek için;

`$ ..\KafkaMessagingQueue.Persistence> dotnet ef database update -s ..\KafkaMessagingQueue.Api\`


**KafkaMessagingQueue.ReportApi** projesinin database'ni migration yapmak için;

`$ ..\KafkaMessagingQueue.ReportApi.Application> dotnet ef migrations add Report-1.0.0 -s ..\KafkaMessagingQueue.ReportApi\`

Var olan migration'u database'e update etmek için;

`$ ..\KafkaMessagingQueue.ReportApi.Application> dotnet ef database update -s ..\KafkaMessagingQueue.ReportApi\`

## Nasıl çalışır?

![alt text](docs/how_does_it_work.png "Nasıl çalışır")
- Client'lar KafkaMessagingQueue.Api'den bir rapor talep ettiğinde. KafkaMessagingQueue.Api Client'a hazırlayacağına dair bir söz döner.
- Bu esnada KafkaMessagingQueue.Api, Client'ın bu isteğini Kafka'ya söyler. Kafkada işlemi kuyruğa atar.
- Kafka bir Topic adıyla Producer (Üretici) eventi oluşturur. KafkaMessagingQueue.Api'de bundan iki tane göreceksiniz.
  - **PreparingEvent;** bu raporu hazırlamaya başladığına dair Cansumer'lara (Tüketicilere) bilgi verir.
  - **CompletedEvent** bu ise raporu tamamladığında bilgi verir.
- Yurıdaki şekilde **HTTP iletişim bölgesi** diye adlandırılmış gri alan KafkaMessagingQueue.Api ve KafkaMessagingQueue.ReportApi arasında HTTP iletişimini ifade eder. Ortada kuyruksal bir iletişim yoktur.