## Reactive Form kullanımı

- Modüle ReactiveFormModule ekle
- component constructor ünde " private formBuilder:FormBuilder " olmalı
- FormGroup türünde bir nesne oolmalı " frm : FormGroup " gibi
- html dosyanda form elementine  " [formGroup]="frm" " ekle 
- inputlarına formControleName lerini gir componentte oluşturduklarınla aynı olmalı adları
- this.frm = this.formBuilder.group({
      adSoyad:[""],
      kullaniciAdi:[""],
      email:[""],
      password:[""],
      passwordConfirm:[""]
    })
  } gibi

- validasyonları  [""] bu kısımlara ikinci parametere olarak dizi türünde giriyoruz
 Örn → adSoyad:["", [Validators.required,Validators.maxLength(50),Validators.minLength(3)]

- [ngClass]="{'is-invalid': submitted && component.adSoyad.errors}" htmlde bunu yazarken hata alıyorsan 
tsconfig.json dosyasındaki 
    "noPropertyAccessFromIndexSignature": false    
    false yap

- buradaki bilgilere karşılık gelecek api ile etkileşim için entities klasörü altında user.ts oluşturdum
