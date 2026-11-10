

\# StudyFlow - Veritabanı Tasarımı



\## 1. Genel Açıklama



StudyFlow uygulamasında veriler Microsoft SQL Server

üzerinde saklanacaktır.



Veritabanı tasarımında kullanıcılar, dersler, konular,

görevler, çalışma planları ve çalışma oturumları

için ayrı tablolar oluşturulması planlanmıştır.



\## 2. Veritabanı Tabloları



\### 2.1. Roles



Kullanıcı rollerini saklar.



\- Id (int, Primary Key)

\- Name (nvarchar(50), Unique)



Örnek roller: Student, Admin



\### 2.2. Users



Sisteme kayıt olan kullanıcıların bilgilerini saklar.



\- Id (int, Primary Key)

\- FullName (nvarchar(100))

\- Email (nvarchar(150), Unique)

\- PasswordHash (nvarchar(255))

\- RoleId (int, Foreign Key)

\- CreatedAt (datetime2)



RoleId alanı Roles tablosuna bağlıdır.



\### 2.3. Categories



Ders kategorilerini saklar.



\- Id (int, Primary Key)

\- Name (nvarchar(100), Unique)

\- Description (nvarchar(500), Nullable)



Kategoriler yönetici tarafından düzenlenebilir.



\### 2.4. Courses



Öğrencilerin eklediği dersleri saklar.



\- Id (int, Primary Key)

\- UserId (int, Foreign Key)

\- CategoryId (int, Foreign Key, Nullable)

\- Name (nvarchar(150))

\- Description (nvarchar(500), Nullable)

\- CreatedAt (datetime2)



UserId alanı Users tablosuna bağlıdır.

CategoryId alanı Categories tablosuna bağlıdır.



\### 2.5. Topics



Derslere ait konuları saklar.



\- Id (int, Primary Key)

\- CourseId (int, Foreign Key)

\- Title (nvarchar(150))

\- IsCompleted (bit)

\- CreatedAt (datetime2)



CourseId alanı Courses tablosuna bağlıdır.



\### 2.6. Tasks



Ödev ve sınav görevlerini saklar.



\- Id (int, Primary Key)

\- UserId (int, Foreign Key)

\- CourseId (int, Foreign Key, Nullable)

\- Title (nvarchar(150))

\- Description (nvarchar(500), Nullable)

\- TaskType (nvarchar(30))

\- DueDate (datetime2, Nullable)

\- IsCompleted (bit)

\- CreatedAt (datetime2)



UserId alanı Users tablosuna bağlıdır.

CourseId alanı Courses tablosuna bağlıdır.



\### 2.7. StudyPlans



Planlanan çalışma zamanlarını saklar.



\- Id (int, Primary Key)

\- UserId (int, Foreign Key)

\- CourseId (int, Foreign Key, Nullable)

\- Title (nvarchar(150))

\- PlannedDate (date)

\- StartTime (time)

\- EndTime (time)

\- Description (nvarchar(500), Nullable)



UserId alanı Users tablosuna bağlıdır.

CourseId alanı Courses tablosuna bağlıdır.



Günlük planlar tarihe göre, haftalık planlar

ise ilgili tarih aralığına göre listelenecektir.



\### 2.8. StudySessions



Gerçekleşen çalışma oturumlarını saklar.



\- Id (int, Primary Key)

\- UserId (int, Foreign Key)

\- CourseId (int, Foreign Key, Nullable)

\- StartedAt (datetime2)

\- EndedAt (datetime2)

\- Notes (nvarchar(500), Nullable)



UserId alanı Users tablosuna bağlıdır.

CourseId alanı Courses tablosuna bağlıdır.



Çalışma süresi başlangıç ve bitiş zamanları

arasındaki farktan hesaplanacaktır.



\## 3. Temel İlişkiler



\- Bir rolün birden fazla kullanıcısı olabilir.

\- Bir kullanıcının birden fazla dersi olabilir.

\- Bir kategoride birden fazla ders bulunabilir.

\- Bir dersin birden fazla konusu olabilir.

\- Bir kullanıcının birden fazla görevi olabilir.

\- Bir kullanıcının birden fazla çalışma planı olabilir.

\- Bir kullanıcının birden fazla çalışma oturumu olabilir.

\- Bir ders birden fazla görev, plan ve oturumla

&#x20; ilişkilendirilebilir.



\## 4. Güvenlik ve Veri Bütünlüğü



\- Kullanıcı şifreleri düz metin saklanmayacaktır.

\- E-posta adresleri benzersiz olacaktır.

\- İlişkiler Foreign Key kullanılarak korunacaktır.

\- Öğrenciler yalnızca kendi verilerine erişebilecektir.

\- Kullanıcı yetkileri backend tarafında kontrol edilecektir.



\## 5. Sonraki Adımlar



Tablo tasarımları ER diyagramı ile gösterilecektir.



Ardından ASP.NET Core ve Entity Framework Core

kullanılarak veritabanı modelleri oluşturulacaktır.



Tablo alanları ve silme davranışları uygulama

geliştirilirken ihtiyaçlara göre netleştirilecektir.



