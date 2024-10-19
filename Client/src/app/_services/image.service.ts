import { Injectable } from "@angular/core";
import { environment } from "../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { Image, ImageAdd} from "../_models/image.module";
import { Observable } from "rxjs";

@Injectable({
  providedIn: 'root'
})

export class ImageService{
  apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  addImage(data: FormData): Observable<any>{
    return this.http.post(this.apiUrl + "/Image/AddImage", data);
  }
  updateImage(data: Image){
    return this.http.put(this.apiUrl + "/Image/UpdateImages", data);
  }
  deleteImage(data: number){
    return this.http.delete(this.apiUrl + "/Image/RemoveImage/" + data);
  }
}
