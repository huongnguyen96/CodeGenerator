
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {ImageFile} from 'models/ImageFile';
import {ImageFileSearch} from 'models/ImageFileSearch';


export class ImageFileMasterRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/image-file/image-file-master');
  }

  public count = (imageFileSearch: ImageFileSearch): Observable<number> => {
    return this.httpService.post('/count',imageFileSearch)
      .pipe(
        map((response: AxiosResponse<number>) => response.data),
      );
  };

  public list = (imageFileSearch: ImageFileSearch): Observable<ImageFile[]> => {
    return this.httpService.post('/list',imageFileSearch)
      .pipe(
        map((response: AxiosResponse<ImageFile[]>) => response.data),
      );
  };

  public get = (id: number): Observable<ImageFile> => {
    return this.httpService.post<ImageFile>('/get', { id })
      .pipe(
        map((response: AxiosResponse<ImageFile>) => response.data),
      );
  };
    
  public delete = (imageFile: ImageFile): Observable<ImageFile> => {
    return this.httpService.post<ImageFile>(`/delete`, imageFile)
      .pipe(
        map((response: AxiosResponse<ImageFile>) => response.data),
      );
  };
  
}

export default new ImageFileMasterRepository();