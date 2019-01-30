# AdminFrontEnd


#Modül açıklamaları

### Dev
	
    "@babel/cli": Babel js için komut satırı
    "@babel/core": Yeni javascript özelliklerinin eski tarayıcılarda ve nodejs ortamında çalışması için dönüşüm yapar
    "@babel/node": Node js için babel eklemeleri
    "@babel/preset-env": Browserlist de belirtilen tarayıcılara yönelik dönüştürme işlemi yapar
    "@babel/preset-react": JSX dosyalarının dönüşümü için destek sağlar
    "@babel/register": Dosyaya çıkartma yapmadan havadan derleme işlemi yapmayı sağlar

	
	"webpack": Uygulamanın bundle edilmesini sağlar. Babel ile ön dönüştürmeyi destekler.
    "webpack-cli": Komut olarak çalıştırılmasını sağlar
    "webpack-dev-server": Dev ortamı için nodejs üzerinde sunucu açar
    "rimraf": Bir klasörü içindekilerle beraber siler, derleme işlemlerinden önce kullanıyoruz.
	"babel-loader": JS dosyalarını Babel üzerinden geçirerek webpack e verir
    "css-loader": JS/JSX/CSS/HTML dosyaları içerisindeki CSS importlarını bulup string olarak okur
    "style-loader": css-loader ile okunan cssi çıktı içerisine koyar
    
    "mocha": Javascript test kütüphanesi
	"ignore-styles": Test sırasında stillerin import edilmemesini sağlar
	"enzyme": React testleri için geliştirilmiş bir kütüphane
    "enzyme-adapter-react-16": React'ın uygun versiyonuna enzyme adaptör kütüphanesi. React'ın yeni sürümleri çıktığında bununda güncellenmesi gerekecektir

	"should": Bir assert kütüphanesi, bir ifadenin belirli koşulları sağlayıp sağlamadını kontrol eder.
    "should-enzyme": Should'a enzyme yönelik yeni koşullar ekler.
	
    "fs-walk": Node js üzerinde bir klasör altındaki dosyalar arasında gezinmeyi sağlar.
    "opn": Node js için işletim sisteminde bir program çalıştırılmasını sağlar.
    "requirejs": Browser testlerinde require kullanılabilmesi için eklendi.
    
    
    
### Prod  

    "react": React !
    "react-dom": React !
    "react-hot-loader": Webpack ile birlikte react componentleri üzerinde değişiklik yapıldığında otomatik yükleme sağlamaktadır. Prod ortamında otomatik olarak çalışmamaktadır.

## Testler

### React Testleri
JSX dosyalarının testleri için enzyme kullanıyoruz https://github.com/airbnb/enzyme
https://github.com/rkotze/should-enzyme
### Kütüphane Testleri
JS dosyları

## Browser Testi
Browserda yapılacak testler için browser-test ler kullanılabilir.
