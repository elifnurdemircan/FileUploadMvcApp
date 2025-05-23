# Dosya Yükleme ve Yönetimi MVC Uygulaması

Bu proje, kullanıcıların güvenli bir şekilde dosya yükleyebileceği, listeleyebileceği ve silebileceği modern bir ASP.NET Core MVC web uygulamasıdır.

## 📋 Dokümantasyon

- 📖 **[README.md](README.md)** - Kurulum ve kullanım kılavuzu (Bu dosya)
- 📊 **[TECHNICAL_REPORT.md](TECHNICAL_REPORT.md)** - Kapsamlı teknik rapor ve analiz

## 🚀 Özellikler

### ✅ Teslim Beklentileri
- **Giriş/Kayıt Ekranı**: Cookie tabanlı güvenli authentication sistemi
- **Dosya Yükleme**: PDF, PNG, JPG formatlarında dosya yükleme (Max: 10MB)
- **Dosya Silme**: Yüklenen dosyaları güvenli bir şekilde silme
- **Dosya Listeleme**: Kullanıcının yüklediği dosyaları görüntüleme
- **Dosya Sunumu**: Yüklenen dosyaları tarayıcıda görüntüleme veya indirme
- **README ve Kurulum Dökümanı**: Detaylı kurulum ve kullanım kılavuzu

### 🎨 Ek Özellikler
- **Modern MVC Mimarisi**: Temiz kod yapısı ve separation of concerns
- **Razor Views**: Server-side rendering ile hızlı sayfa yükleme
- **Bootstrap 5**: Responsive ve modern UI tasarımı
- **Gradient Tasarım**: Görsel olarak çekici arka plan ve buton tasarımları
- **Font Awesome İkonlar**: Profesyonel görünüm
- **Form Validasyonu**: Client-side ve server-side doğrulama
- **Dosya Boyutu Gösterimi**: Kullanıcı dostu bilgiler
- **Tarih Bilgisi**: Dosya yükleme tarihlerinin gösterimi

## 🛠️ Teknolojiler

### Backend
- **ASP.NET Core 8.0 MVC**: Modern web framework
- **Entity Framework Core**: In-Memory database
- **Cookie Authentication**: Güvenli session yönetimi
- **Razor Pages**: Server-side rendering

### Frontend
- **Razor Views**: Server-side HTML generation
- **Bootstrap 5**: Responsive UI framework
- **Font Awesome**: İkon kütüphanesi
- **CSS3**: Modern gradient tasarımlar
- **JavaScript**: Form etkileşimleri

## 📋 Gereksinimler

- .NET 8.0 SDK
- Modern web tarayıcısı (Chrome, Firefox, Safari, Edge)

## 🚀 Kurulum

### 1. Projeyi İndirin
```bash
git clone [repository-url]
cd FileUploadMvcApp
```

### 2. Bağımlılıkları Yükleyin
```bash
dotnet restore
```

### 3. Uygulamayı Çalıştırın
```bash
dotnet run
```

### 4. Tarayıcıda Açın
Uygulama varsayılan olarak şu adreste çalışacaktır:
- HTTP: `http://localhost:5000`
- HTTPS: `https://localhost:5001`

## 📖 Kullanım Kılavuzu

### 1. Hesap Oluşturma
- Ana sayfada "Hesabınız yok mu? Kayıt olun" linkine tıklayın
- E-posta, kullanıcı adı ve şifre bilgilerinizi girin
- Şifre tekrarını doğru girin
- "Kayıt Ol" butonuna tıklayın

### 2. Giriş Yapma
- Kullanıcı adı ve şifrenizi girin
- "Giriş Yap" butonuna tıklayın

### 3. Dosya Yükleme
- Giriş yaptıktan sonra "Dosya seçmek için tıklayın" alanına tıklayın
- PDF, PNG veya JPG formatında bir dosya seçin (Max: 10MB)
- Dosya otomatik olarak yüklenecektir

### 4. Dosya Yönetimi
- Yüklenen dosyalar "Dosyalarım" bölümünde listelenir
- 👁️ ikonu ile dosyayı tarayıcıda görüntüleyebilirsiniz
- 📥 ikonu ile dosyayı indirebilirsiniz
- 🗑️ ikonu ile dosyayı silebilirsiniz

## 🏗️ Proje Mimarisi

### MVC Pattern
- **Models**: Veri modelleri ve view modelleri
- **Views**: Razor view'ları ve layout
- **Controllers**: İş mantığı ve HTTP istekleri

### Klasör Yapısı
```
FileUploadMvcApp/
├── Controllers/
│   ├── AccountController.cs      # Authentication işlemleri
│   └── FileUploadController.cs   # Dosya yönetimi
├── Models/
│   ├── User.cs                  # User entity
│   ├── UploadedFile.cs          # File entity
│   ├── LoginViewModel.cs        # Login form model
│   ├── RegisterViewModel.cs     # Register form model
│   └── FileUploadViewModel.cs   # File upload page model
├── Views/
│   ├── Account/
│   │   ├── Login.cshtml         # Giriş sayfası
│   │   └── Register.cshtml      # Kayıt sayfası
│   ├── FileUpload/
│   │   └── Index.cshtml         # Ana dosya yönetimi sayfası
│   └── Shared/
│       └── _Layout.cshtml       # Ana layout
├── Data/
│   └── AppDbContext.cs          # Entity Framework context
├── Services/
│   └── FileService.cs           # Dosya işlemleri servisi
├── wwwroot/
│   └── uploads/                 # Yüklenen dosyalar
├── Program.cs                   # Uygulama yapılandırması
├── README.md                    # Bu dosya
└── TECHNICAL_REPORT.md          # Teknik rapor
```

## 🔒 Güvenlik

- **Cookie Authentication**: Güvenli session yönetimi
- **Form Validation**: Client ve server-side doğrulama
- **File Validation**: Dosya türü ve boyut kontrolü
- **User Isolation**: Her kullanıcı sadece kendi dosyalarını görebilir
- **CSRF Protection**: Cross-site request forgery koruması

## 🎯 Önemli Notlar

1. **In-Memory Database**: Uygulama in-memory database kullanır, yeniden başlatıldığında veriler silinir
2. **Dosya Depolama**: Dosyalar `wwwroot/uploads` klasöründe saklanır
3. **Session Management**: Cookie tabanlı authentication kullanılır
4. **File Size Limit**: Maksimum 10MB dosya boyutu sınırı
5. **Supported Formats**: Sadece PDF, PNG, JPG dosyaları desteklenir

## 🐛 Sorun Giderme

### Port Çakışması
Eğer port 5000 veya 5001 kullanımda ise:
```bash
dotnet run --urls="http://localhost:8080;https://localhost:8081"
```

### Dosya Yükleme Hatası
- Dosya boyutunun 10MB'dan küçük olduğundan emin olun
- Sadece PDF, PNG, JPG formatlarının desteklendiğini kontrol edin
- Tarayıcı console'unda hata mesajlarını kontrol edin

### Authentication Hatası
- Cookie'lerin etkin olduğundan emin olun
- Tarayıcı cache'ini temizleyin
- Gizli/özel tarama modunda test edin

## 🆚 API vs MVC Karşılaştırması

### MVC Avantajları:
- **Server-side Rendering**: Daha hızlı ilk sayfa yükleme
- **SEO Friendly**: Arama motorları için optimize
- **Basit Deployment**: Tek uygulama olarak deploy
- **Form Handling**: Built-in form validation ve model binding

### API Avantajları:
- **Flexibility**: Farklı client'lar için kullanılabilir
- **Scalability**: Microservice mimarisine uygun
- **Modern**: SPA uygulamaları için ideal

## 📊 Teknik Analiz

Detaylı teknik analiz, performans metrikleri, güvenlik değerlendirmesi ve gelecek geliştirme önerileri için **[Teknik Rapor](TECHNICAL_REPORT.md)** dosyasını inceleyiniz.

### Rapor İçeriği:
- 🔧 **Teknik Spesifikasyonlar** - Kullanılan teknolojiler ve versiyonlar
- 🏗️ **Mimari Analizi** - MVC pattern ve katmanlı mimari detayları  
- 🔒 **Güvenlik Değerlendirmesi** - Vulnerability assessment ve öneriler
- ⚡ **Performans Metrikleri** - Startup time, memory usage, response times
- 🧪 **Test Sonuçları** - Functional, compatibility ve responsive testing
- 📈 **Geliştirme Önerileri** - Kısa, orta ve uzun vadeli iyileştirmeler

## 📞 Destek

Herhangi bir sorun yaşarsanız, lütfen GitHub Issues bölümünden bildiriniz.