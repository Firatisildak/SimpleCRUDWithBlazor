# SimpleCRUD Projesi

## Proje Açıklaması (Project Description)
**SimpleCRUD** projesi, çalışan bilgilerini yönetmek için temel CRUD (Create, Read, Update, Delete) işlemleri gerçekleştiren bir uygulamadır. Bu uygulama, ASP.NET Core, Entity Framework Core, ve ClosedXML teknolojileri kullanılarak geliştirilmiştir. Ayrıca, çalışan bilgilerini Excel dosyasına dışa aktarabilme ve dışarıdan verileri içe aktarabilme gibi özellikler de mevcuttur.

### Teknolojiler (Technologies Used)
- **ASP.NET Core**: Web API oluşturulmasında kullanıldı.
- **Entity Framework Core**: Veritabanı işlemleri için ORM kullanıldı.
- **ClosedXML**: Excel dosyalarını oluşturmak ve işlemek için kullanıldı.
- **EFCore.BulkExtensions**: Veritabanına toplu veri işlemleri yapmak için kullanıldı.
- **Microsoft.EntityFrameworkCore**: Veritabanı bağlantısı ve işlemleri için kullanıldı.

---

## Katmanlar (Project Layers)

### 1. **Entities**
Veritabanı tablolarını temsil eden sınıflar bulunur. Örneğin, `Employee` sınıfı, çalışanın tüm bilgilerini tutar.

### 2. **DataAccess**
Veritabanı işlemleri için kullanılan `DbContext` sınıfı burada yer alır. 
- `AppDBContext` sınıfı, EF Core için yapılandırılmıştır.

### 3. **Services**
CRUD işlemleri ve veri dışa/içe aktarma işlemleri burada yer alır.
- `EmployeeService` sınıfı:
  - Çalışanları ekleme, güncelleme, silme ve listeleme.
  - Çalışan verilerini Excel dosyasına dışa aktarma ve içe aktarma işlemleri.

### 4. **ViewModels**
API ile kullanılan veri modelleridir.
- `EmployeeViewModel`, kullanıcı dostu veri modelleri sağlar.

### 5. **Enums**
Uygulama içindeki sabitleri tanımlar.
- `UIActionEnum`: Insert ve Update gibi işlemleri tanımlar.

---

## Özellikler (Features)

1. **CRUD İşlemleri**:
   - Çalışan ekleme, güncelleme, silme ve listeleme.

2. **Veri Dışa Aktarma**:
   - Çalışan bilgilerini Excel dosyasına dışa aktarabilirsiniz.

3. **Veri İçe Aktarma**:
   - Excel dosyasından verileri içe aktarabilirsiniz.

4. **Çalışan ID Formatı**:
   - `EmployeeId` özelliği, `EMPXXXX` formatında gösterilir.

