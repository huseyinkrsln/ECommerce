import { Token } from "@angular/compiler";


// Bu sınıfın oluşturulma sebebi localStorage da tokenı görmemesi.
// yani console da Ağ kısmında baktğımızda varken local storage da yok ama böyle bir atama yaparak sorun çözüldü

export class TokenResponse{
  token : Token;
}
