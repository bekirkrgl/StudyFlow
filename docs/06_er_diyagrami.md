

\# StudyFlow - ER Diyagramı



\## 1. Veritabanı İlişki Diyagramı



StudyFlow uygulaması için 8 temel tablo planlanmıştır.

Tablolar arasındaki ilişkiler aşağıda gösterilmiştir.



```mermaid

erDiagram

&#x20;   Roles ||--o{ Users : has

&#x20;   Users ||--o{ Courses : owns

&#x20;   Categories o|--o{ Courses : categorizes

&#x20;   Courses ||--o{ Topics : contains

&#x20;   Users ||--o{ Tasks : creates

&#x20;   Courses o|--o{ Tasks : relates\_to

&#x20;   Users ||--o{ StudyPlans : creates

&#x20;   Courses o|--o{ StudyPlans : relates\_to

&#x20;   Users ||--o{ StudySessions : records

&#x20;   Courses o|--o{ StudySessions : relates\_to



&#x20;   Roles {

&#x20;       int Id PK

&#x20;       nvarchar(50) Name UK

&#x20;   }



&#x20;   Users {

&#x20;       int Id PK

&#x20;       nvarchar(100) FullName

&#x20;       nvarchar(150) Email UK

&#x20;       nvarchar(255) PasswordHash

&#x20;       int RoleId FK

&#x20;       datetime2 CreatedAt

&#x20;   }



&#x20;   Categories {

&#x20;       int Id PK

&#x20;       nvarchar(100) Name UK

&#x20;       nvarchar(500) Description "nullable"

&#x20;   }



&#x20;   Courses {

&#x20;       int Id PK

&#x20;       int UserId FK

&#x20;       int CategoryId FK "nullable"

&#x20;       nvarchar(150) Name

&#x20;       nvarchar(500) Description "nullable"

&#x20;       datetime2 CreatedAt

&#x20;   }



&#x20;   Topics {

&#x20;       int Id PK

&#x20;       int CourseId FK

&#x20;       nvarchar(150) Title

&#x20;       bit IsCompleted

&#x20;       datetime2 CreatedAt

&#x20;   }



&#x20;   Tasks {

&#x20;       int Id PK

&#x20;       int UserId FK

&#x20;       int CourseId FK "nullable"

&#x20;       nvarchar(150) Title

&#x20;       nvarchar(500) Description "nullable"

&#x20;       nvarchar(30) TaskType

&#x20;       datetime2 DueDate "nullable"

&#x20;       bit IsCompleted

&#x20;       datetime2 CreatedAt

&#x20;   }



&#x20;   StudyPlans {

&#x20;       int Id PK

&#x20;       int UserId FK

&#x20;       int CourseId FK "nullable"

&#x20;       nvarchar(150) Title

&#x20;       date PlannedDate

&#x20;       time StartTime

&#x20;       time EndTime

&#x20;       nvarchar(500) Description "nullable"

&#x20;   }



&#x20;   StudySessions {

&#x20;       int Id PK

&#x20;       int UserId FK

&#x20;       int CourseId FK "nullable"

&#x20;       datetime2 StartedAt

&#x20;       datetime2 EndedAt

&#x20;       nvarchar(500) Notes "nullable"

&#x20;   }

```



\## 2. İlişkilerin Açıklaması



\- Roles - Users: Bir rolün birçok kullanıcısı olabilir.

\- Users - Courses: Bir kullanıcının birçok dersi olabilir.

\- Categories - Courses: Bir kategoride birçok ders olabilir.

\- Courses - Topics: Bir derse birçok konu eklenebilir.

\- Users - Tasks: Bir kullanıcı birçok görev oluşturabilir.

\- Courses - Tasks: Görev isteğe bağlı olarak bir derse bağlanabilir.

\- Users - StudyPlans: Bir kullanıcı birçok çalışma planı oluşturabilir.

\- Courses - StudyPlans: Plan isteğe bağlı olarak bir derse bağlanabilir.

\- Users - StudySessions: Bir kullanıcının birçok çalışma kaydı olabilir.

\- Courses - StudySessions: Oturum isteğe bağlı olarak bir derse bağlanabilir.



\## 3. Anahtarların Anlamları



\- PK (Primary Key): Tablodaki kaydın benzersiz anahtarıdır.

\- FK (Foreign Key): Başka bir tabloya referans verir.

\- UK (Unique Key): Bir alanın benzersiz olmasını sağlar.



\## 4. Tasarım Notları



Bir kullanıcı birden fazla ders, görev ve çalışma

oturumuna sahip olabilir.



Derslere bağlı görev ve çalışma kayıtlarında

CourseId alanı isteğe bağlıdır.



Bir kullanıcıya ait görev veya çalışma kaydı,

başka bir kullanıcının dersine bağlanmamalıdır.

Bu durum backend tarafında kontrol edilecektir.



Veritabanı ilişkileri ve silme davranışları

Entity Framework Core ile uygulanırken

ayrıntılı olarak yapılandırılacaktır.



