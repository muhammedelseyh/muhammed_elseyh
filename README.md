# Seyahat Rezervasyon Sistemi Simülasyonu

## Giriş

Bu yazılım, bir seyahat rezervasyon sistemini simüle etmek amacıyla nesne yönelimli programlama (OOP) ilkelerine dayanarak geliştirilmiştir. Uygulama, Yolcu, Bilet ve Taşıma gibi temel kavramları modelleyerek kullanıcılara bilet alımı, iptali ve görüntülenmesi gibi işlemleri sunar. Aşağıda, kullanılan OOP ilkelerinin ve Bağımlılık Tersine Çevirme İlkesi (Dependency Inversion Principle)'nin nasıl uygulandığına dair bir açıklama yer almaktadır.

## OOP İlkeleri

### Kalıtım (Inheritance)

Kalıtım, nesne yönelimli programlamada bir sınıfın başka bir sınıftan türemesi ve ondan özellikleri ve yöntemleri miras almasıdır. Bu yazılımda kalıtım, aşağıdaki şekilde kullanılmıştır:

*   `Person` sınıfı, temel kişisel bilgileri (isim, e-posta, telefon) içerir ve `Passenger` sınıfı bu sınıftan türetilir. `Passenger` sınıfı, ek bir özellik olan pasaport numarasını içerir ve bu sınıfın `DisplayDetails` metodunu geçersiz kılarak yolcunun tüm bilgilerini görüntüler.
*   `Ticket` sınıfı, biletlerin temel özelliklerini (bilet numarası, yolcu, varış noktası, tarih ve fiyat) tanımlar ve `EconomyTicket` ile `BusinessTicket` sınıflarına kalıtım yoluyla bu özellikleri devreder. Her iki sınıf da `Ticket` sınıfındaki soyut `CalculatePrice` metodunu kendilerine özgü şekilde uygular.

### Kapsülleme (Encapsulation)

Kapsülleme, veriyi ve işlemleri bir sınıf içinde gizlemeyi, dışarıdan erişimi sınırlamayı sağlar. Bu yazılımda kapsülleme şu şekillerde uygulanmıştır:

*   `Person` sınıfındaki `Name`, `Email` ve `Phone` gibi özellikler yalnızca sınıf içinde ayarlanabilir ve dışarıdan yalnızca `get` metotlarıyla okunabilir. Bu, sınıfın iç verilerini kontrol altına alır ve dışarıdan gereksiz erişimi engeller.
*   `Ticket` ve `Transportation` sınıflarındaki bazı özellikler (örneğin, `TicketNumber`, `BasePrice`, `VehicleNumber`) yalnızca constructor ile belirlenebilir ve dışarıdan yalnızca `get` metotlarıyla erişilebilir. Bu da sınıfların iç durumlarını dışarıdan müdahaleye karşı korur.

### Polimorfizm (Polymorphism)

Polimorfizm, bir sınıfın farklı türlerinin aynı arayüzü kullanarak farklı davranmalarını sağlar. Bu yazılımda polimorfizm şu şekilde kullanılmıştır:

*   `Ticket` sınıfı, `EconomyTicket` ve `BusinessTicket` gibi türetilmiş sınıflar tarafından `CalculatePrice` metodunun farklı bir şekilde uygulanmasını sağlar. Bu sayede, her bilet türü, temel fiyat hesaplamasını kendi kurallarına göre yapar.
*   `DisplayTicketDetails` metodunun `Ticket` sınıfında tanımlanması ve türetilmiş sınıflar tarafından değiştirilmeden kullanılması da bir polimorfizm örneğidir. Her iki bilet türü, aynı metodu çağırarak kendi bilgilerini ekler.

### Soyutlama (Abstraction)

Soyutlama, karmaşık sistemlerin yalnızca önemli özelliklerini ortaya koyar ve gereksiz detaylardan kaçınır. Bu yazılımda soyutlama şu şekilde uygulanmıştır:

*   `Ticket` ve `Transportation` sınıfları soyut sınıflardır. Bu sınıflar, her iki bileti ve taşıma aracını tanımlayan temel özellikleri ve metodları içerir, ancak doğrudan kullanılamazlar. `EconomyTicket` ve `BusinessTicket` sınıfları gibi somut sınıflar, soyut sınıflardan türetilir ve soyut metodları kendilerine özgü şekilde gerçekleştirir.
*   `ITicket` arayüzü, biletlerle ilgili tüm işlemleri soyutlar. `BookingManager` sınıfı yalnızca `ITicket` arayüzüne bağımlıdır ve somut sınıfların detaylarıyla ilgilenmez.

## Bağımlılık Tersine Çevirme İlkesi (Dependency Inversion Principle)

Bağımlılık Tersine Çevirme İlkesi, yüksek seviye modüllerin düşük seviye modüllere değil, her ikisinin de soyutlamalara (interface veya abstract class) bağımlı olması gerektiğini belirtir. Bu yazılımda, bu ilke şu şekilde uygulanmıştır:

*   `BookingManager` sınıfı, biletlerin somut türlerine (örneğin, `EconomyTicket` veya `BusinessTicket`) doğrudan bağımlı değildir. Bunun yerine, `ITicket` arayüzüne bağımlıdır. Bu, yüksek seviye modül olan `BookingManager`'ın, düşük seviye modüllerin (bilet türlerinin) detaylarına bağımlı olmasını engeller ve sistemin genişletilmesini kolaylaştırır.
*   `TicketFactory` sınıfı, biletlerin oluşturulmasını sorumlu tutarak `BookingManager`'ı somut sınıflardan bağımsız hale getirir. `TicketFactory`, `ITicket` türünde biletler oluşturur ve `BookingManager` bu biletleri yalnızca arayüzü kullanarak yönetir. Bu sayede, yeni bilet türleri eklendiğinde, `BookingManager` sınıfında herhangi bir değişiklik yapmaya gerek kalmaz.

## Sonuç

Bu yazılımda, nesne yönelimli programlamanın temel ilkeleri (kalıtım, kapsülleme, polimorfizm ve soyutlama) başarılı bir şekilde uygulanmış ve Bağımlılık Tersine Çevirme İlkesi ile yazılımın esnekliği artırılmıştır. Bu yapı sayesinde, sistemin yeni bilet türleri veya taşıma modları eklenmesi durumunda `BookingManager` sınıfında değişiklik yapılması gerekmemekte ve yazılım daha kolay bakım yapılabilir hale gelmektedir.
