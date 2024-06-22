# Not Kayıt Otomasyonu

Bu proje, öğrencilerin ve öğretmenlerin notlarını görüntüleyebileceği ve yönetebileceği bir otomasyon sistemidir. Aşağıda, projedeki ana dosyaların açıklamaları bulunmaktadır.

## FrmGiris.cs

### FrmGiris Sınıfı
Bu sınıf, kullanıcıların sisteme giriş yapmasını sağlar. Kullanıcı giriş yaparken öğrenci veya öğretmen olmasına göre farklı formlara yönlendirilir.

### FrmGiris()
Formun yapıcı (constructor) metodudur. Form bileşenlerini başlatır (`InitializeComponent` metodu çağrılır).

### button1_Click
- **Amaç:** Kullanıcı, butona tıkladığında `FrmOgrenciDetay` formu açılır.
- **İşleyiş:**
  - Yeni bir `FrmOgrenciDetay` nesnesi oluşturulur.
  - `frm.numara` özelliği, `maskedTextBox1` kontrolünden alınan metinle (öğrenci numarası) doldurulur.
  - `frm` formu görüntülenir (`Show` metodu ile).

### maskedTextBox1_TextChanged
- **Amaç:** Kullanıcının girdiği metin `maskedTextBox1` kontrolünde değiştiğinde çağrılır.
- **İşleyiş:**
  - `maskedTextBox1` kontrolündeki metin "1111" ise (bu muhtemelen bir öğretmen kodudur), `FrmOgretmenDetay` formu açılır.
  - Yeni bir `FrmOgretmenDetay` nesnesi oluşturulur.
  - `fr` formu görüntülenir (`Show` metodu ile).
### FrmGiris() Örnek Ekran Görüntüsü
![FrmGiris](https://github.com/huseynaktas/Not_Kayit_Otomasyonu/assets/114494075/35c3ebcb-830c-4f2f-8647-2eb8a20f38b4)


## FrmOgrenciDetay.cs

### FrmOgrenciDetay Sınıfı
Bu sınıf, öğrencinin not ve diğer detaylarını görüntüler. Öğrenci, numarası ile giriş yaptıktan sonra detaylarını bu formda görür.

### FrmOgrenciDetay()
Formun yapıcı (constructor) metodudur. Form bileşenlerini başlatır (`InitializeComponent` metodu çağrılır).

### SqlConnection conn
- **Amaç:** Veritabanı bağlantısını temsil eder.
- **Detay:** Veritabanına bağlanmak için gerekli bağlantı dizesini (`connection string`) içerir.

### public string numara
- **Amaç:** Öğrenci numarasını tutar.
- **Detay:** Bu numara, `FrmGiris` formundan alınır ve öğrenci detaylarını çekmek için kullanılır.

### FrmOgrenciDetay_Load
- **Amaç:** Form yüklendiğinde öğrenci bilgilerini veritabanından çekip form üzerinde görüntüler.
- **İşleyiş:**
  - `lblNumara` etiketi, `numara` değişkeni ile doldurulur.
  - Veritabanı bağlantısı açılır (`conn.Open`).
  - `SqlCommand` nesnesi oluşturulur ve `OGRNUMARA` alanına `numara` parametresi eklenir.
  - `SqlDataReader` ile sorgu sonuçları okunur.
  - Sorgu sonuçları, ilgili etiketlere (`lblAdSoyad`, `lblS1`, `lblS2`, `lblS3`, `lblOrt`, `lblDurum`) doldurulur.
  - Veritabanı bağlantısı kapatılır (`conn.Close`).
### FrmOgrenciDetay() Örnek Ekran Görüntüsü
![FrmOgrenciDetay](https://github.com/huseynaktas/Not_Kayit_Otomasyonu/assets/114494075/9eacea9f-b2a3-43f3-9b00-d96597e0223c)


## FrmOgretmenDetay.cs

### FrmOgretmenDetay Sınıfı
Bu sınıf, öğretmenin öğrencilerin notlarını yönetmesini sağlar. Öğretmen, öğrenci bilgilerini güncelleyebilir veya yeni öğrenci ekleyebilir.

### FrmOgretmenDetay()
Formun yapıcı (constructor) metodudur. Form bileşenlerini başlatır (`InitializeComponent` metodu çağrılır).

### SqlConnection conn
- **Amaç:** Veritabanı bağlantısını temsil eder.
- **Detay:** Veritabanına bağlanmak için gerekli bağlantı dizesini (`connection string`) içerir.

### FrmOgretmenDetay_Load
- **Amaç:** Form yüklendiğinde veritabanından ders bilgilerini getirir.
- **İşleyiş:**
  - `this.tBLDERSTableAdapter.Fill` metodu çağrılarak `TBLDERS` tablosundan veri çekilir ve formdaki `DataGridView` kontrolüne doldurulur.

### button1_Click
- **Amaç:** Yeni bir öğrenci eklemek için kullanılır.
- **İşleyiş:**
  - Veritabanı bağlantısı açılır (`conn.Open`).
  - `SqlCommand` nesnesi oluşturulur ve gerekli parametreler (`@P1`, `@P2`, `@P3`) eklenir.
  - Komut yürütülür (`ExecuteNonQuery`).
  - Veritabanı bağlantısı kapatılır (`conn.Close`).
  - Kullanıcıya bir mesaj gösterilir (`MessageBox.Show`).
  - `TBLDERS` tablosu tekrar doldurulur (`this.tBLDERSTableAdapter.Fill`).

### dataGridView1_CellClick
- **Amaç:** DataGridView'deki bir hücreye tıklandığında, ilgili öğrencinin bilgilerini formdaki alanlara doldurur.
- **İşleyiş:**
  - Seçilen satırın indeksi alınır (`secilen`).
  - İlgili hücre değerleri, formdaki metin kutularına (`mskNumara`, `txtAd`, `txtSoyad`, `txtS1`, `txtS2`, `txtS3`) doldurulur.

### public string durum
- **Amaç:** Öğrencinin geçme durumunu tutar.
- **Detay:** Ortalamaya göre "True" (geçti) veya "False" (kaldı) değerini alır.

### btnGuncelle_Click
- **Amaç:** Öğrencinin notlarını güncellemek ve ortalamayı hesaplamak için kullanılır.
- **İşleyiş:**
  - Notlar (`txtS1`, `txtS2`, `txtS3`) alınır ve ortalama hesaplanır.
  - Ortalama, `lblOrt` etiketine doldurulur.
  - Ortalama 50 ve üzeri ise `durum` "True", değilse "False" olur.
  - Veritabanı bağlantısı açılır (`conn.Open`).
  - `SqlCommand` nesnesi oluşturulur ve gerekli parametreler (`@P1`, `@P2`, `@P3`, `@P4`, `@P5`, `@P6`) eklenir.
  - Komut yürütülür (`ExecuteNonQuery`).
  - Veritabanı bağlantısı kapatılır (`conn.Close`).
  - Kullanıcıya bir mesaj gösterilir (`MessageBox.Show`).
  - `TBLDERS` tablosu tekrar doldurulur (`this.tBLDERSTableAdapter.Fill`).
### FrmOgretmenDetay() Örnek Ekran Görüntüsü
![FrmOgretmenDetay](https://github.com/huseynaktas/Not_Kayit_Otomasyonu/assets/114494075/3a420415-8c2c-49c4-b0c9-5047c55016cc)

## Kullanılan Veritabanının Diyagramı
![Veritabanı](https://github.com/huseynaktas/Not_Kayit_Otomasyonu/assets/114494075/906a0fa7-8e62-496e-8f6e-b0548a91a747)
