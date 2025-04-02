# 🛠️ Sipariş Worker Servisi

Bu proje, belirli aralıklarla (5 dakikada bir) dış bir API'den sipariş listesi çekmek için geliştirilmiş bir **ASP.NET Core Worker Service** uygulamasıdır.  
Servis, API'ye erişmeden önce token almak zorundadır ve token isteklerinde rate-limit (1 saatte max 5 istek) kısıtlaması vardır.

---

## 🔧 Proje Özellikleri

- ⏱️ **Background worker**: Her 5 dakikada bir çalışır
- 🔐 **Token yönetimi**: Token alındıktan sonra `MemoryCache` ile 1 saat saklanır
- 📦 **REST API'den veri çekme**: `Authorization` header'ı ile veri alır
- 🧪 **Test ortamı**: Gerçek API olmadan `reqres.in` ve `jsonplaceholder.typicode.com` üzerinden test yapılabilir

---

## 🚀 Kurulum

### 1. Projeyi Klonla
```bash
git clone https://github.com/kullaniciadi/SiparisWorker.git
cd SiparisWorker

### 2. Gerekli Paketleri Yükle
dotnet restore

### 3. Projeyi Çalıştır
dotnet run
