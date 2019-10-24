
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {ImageFile} from 'models/ImageFile';
import {ImageFileSearch} from 'models/ImageFileSearch';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';

export class ImageFileDetailRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/image-file/image-file-detail');
  }

  public get = (id: number): Observable<ImageFile> => {
    return this.httpService.post<ImageFile>('/get', { id })
      .pipe(
        map((response: AxiosResponse<ImageFile>) => response.data),
      );
  };

  public create = (imageFile: ImageFile): Observable<ImageFile> => {
    return this.httpService.post<ImageFile>(`/create`, imageFile)
      .pipe(
        map((response: AxiosResponse<ImageFile>) => response.data),
      );
  };
  public update = (imageFile: ImageFile): Observable<ImageFile> => {
    return this.httpService.post<ImageFile>(`/update`, imageFile)
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

  public save = (imageFile: ImageFile): Observable<ImageFile> => {
    return imageFile.id ? this.update(imageFile) : this.create(imageFile);
  };

}

export default new ImageFileDetailRepository();
