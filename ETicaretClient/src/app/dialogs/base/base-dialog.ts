import { MatDialogRef } from "@angular/material/dialog";

//bu sınıf oluşmasının sebebi dialogları evreslleştirmek ve her dialogda olacak olan (silinsin mi ?) gibi tekrar edecek unsurları buraya ekliyoruz
export class BaseDialog<T> {
constructor(
  public dialogRef: MatDialogRef<T>,
  ){}
  close(){
    this.dialogRef.close();
  }
}
