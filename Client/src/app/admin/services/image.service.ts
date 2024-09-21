import { Injectable } from "@angular/core";
import { environment } from "../../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { Image} from "../models/image.module";

@Injectable({
  providedIn: 'root'
})

export class ImageService{
  apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  addImage(data: Image){
    return this.http.post(this.apiUrl + "/Image/AddImage", data);
  }
  updateImage(data: Image){
    return this.http.put(this.apiUrl + "/Image/UpdateImages", data);
  }
}
