

\# StudyFlow - Teknolojiler ve Sistem Mimarisi



\## 1. Kullanılacak Teknolojiler



StudyFlow projesi mobil uygulama, backend ve

veritabanı olmak üzere üç temel katmandan

oluşacak şekilde planlanmıştır.



\### 1.1. Flutter ve Dart



Mobil uygulama Flutter framework'ü ve Dart

programlama dili kullanılarak geliştirilecektir.



Flutter, kullanıcı arayüzlerinin hazırlanması

ve Android uygulamasının geliştirilmesi için

tercih edilmiştir.



\### 1.2. ASP.NET Core Web API



Backend tarafında C# dili ve ASP.NET Core

Web API kullanılacaktır.



API, mobil uygulamadan gelen istekleri

karşılayacak, gerekli kontrolleri yapacak

ve veritabanı işlemlerini yönetecektir.



\### 1.3. Microsoft SQL Server



Kullanıcılar, dersler, konular, görevler

ve çalışma kayıtları SQL Server

veritabanında tutulacaktır.



\### 1.4. Entity Framework Core



Backend ile veritabanı arasındaki işlemlerde

Entity Framework Core kullanılacaktır.



Bu teknoloji sayesinde veritabanı

tablolarıyla C# sınıfları üzerinden

çalışılması planlanmaktadır.



\### 1.5. JWT



Kullanıcıların kimlik doğrulama işlemleri

için JWT tabanlı bir yapı kullanılacaktır.



API üzerinde kullanıcıların yetkileri

kontrol edilecek ve öğrencilerin sadece

kendilerine ait verilere erişmesi

sağlanacaktır.



\### 1.6. Swagger



Geliştirilen API endpoint'lerinin

belgelenmesi ve test edilmesi için

Swagger kullanılacaktır.



\### 1.7. Git ve GitHub



Projenin sürüm kontrolü Git ile yapılacak

ve geliştirme süreci GitHub üzerinden

takip edilecektir.



\## 2. Genel Sistem Mimarisi



StudyFlow üç katmanlı bir yapıda

geliştirilecektir.



Flutter Mobil Uygulaması

&#x20;         |

&#x20;         | HTTPS / REST API (JSON)

&#x20;         |

&#x20;         v

ASP.NET Core Web API

&#x20;         |

&#x20;         | Entity Framework Core

&#x20;         |

&#x20;         v

Microsoft SQL Server



\### 2.1. Mobil Uygulama Katmanı



Kullanıcıların etkileşim kuracağı ekranlar

bu katmanda yer alacaktır.



Ders ekleme, görev oluşturma, çalışma planı

hazırlama ve istatistik görüntüleme

işlemleri mobil uygulamadan başlatılacaktır.



\### 2.2. Backend Katmanı



Mobil uygulamadan gelen istekler bu

katmanda işlenecektir.



Kullanıcı doğrulama, yetkilendirme,

veri kontrolü ve veritabanı işlemleri

backend tarafından gerçekleştirilecektir.



\### 2.3. Veritabanı Katmanı



Uygulamaya ait veriler SQL Server

üzerinde saklanacaktır.



Veritabanı tabloları ve ilişkileri

projenin ikinci haftasında

ayrıntılı olarak tasarlanacaktır.



\## 3. Örnek Veri Akışı



Bir öğrencinin ders ekleme işlemi

aşağıdaki şekilde gerçekleşecektir:



1\. Öğrenci Flutter uygulamasında ders

&#x20;  bilgilerini girer.



2\. Flutter, bilgileri API'ye gönderir.



3\. API, kullanıcı yetkisini ve

&#x20;  gönderilen bilgileri kontrol eder.



4\. Entity Framework Core aracılığıyla

&#x20;  ders kaydı SQL Server'a eklenir.



5\. API işlem sonucunu Flutter'a döndürür.



6\. Flutter sonucu kullanıcıya gösterir.



\## 4. Geliştirme Önceliği



Proje öncelikli olarak Android platformu

için geliştirilecektir.



İlk aşamada analiz ve planlama,

ardından veritabanı ve backend,

daha sonra mobil uygulama geliştirmesi

gerçekleştirilecektir.





