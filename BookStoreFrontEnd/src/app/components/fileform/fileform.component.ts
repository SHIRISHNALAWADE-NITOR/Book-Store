import { Component, OnInit } from '@angular/core';
import { FileService } from 'src/app/services/file.service'; // Adjust path as necessary
import { ActivatedRoute } from '@angular/router';
 
@Component({
  selector: 'app-fileform',
  templateUrl: './fileform.component.html',
  styleUrls: ['./fileform.component.css']
})
export class FileformComponent implements OnInit {
  bookId: number = 0;
  audioFile: File | null = null;
  videoFile: File | null = null;
  pdfFile: File | null = null;
  files: any[] = [];
 
  constructor(
    private fileService: FileService,
    private route: ActivatedRoute
  ) {}
 
  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.bookId = +params.get('id')!;
      this.loadFiles();
    });
  }
 
  loadFiles() {
    this.files = []; // Clear the existing files array before loading new files
 
    // Load audio file
    this.fileService.getAudioFile(this.bookId).subscribe({
      next: (blob: Blob) => {
        const audio = new File([blob], 'audio.mp3', { type: 'audio/mp3' });
        this.files.push({
          id: this.bookId,
          name: 'Audio File',
          file: audio,
          type: 'audio',
          url: URL.createObjectURL(audio)
        });
        console.log('Audio file loaded successfully');
      },
      error: (error: any) => {
        console.error('Error loading audio file!', error);
      }
    });
 
    // Load video file
    this.fileService.getVideoFile(this.bookId).subscribe({
      next: (blob: Blob) => {
        const video = new File([blob], 'video.mp4', { type: 'video/mp4' });
        this.files.push({
          id: this.bookId,
          name: 'Video File',
          file: video,
          type: 'video',
          url: URL.createObjectURL(video)
        });
        console.log('Video file loaded successfully');
      },
      error: (error: any) => {
        console.error('Error loading video file!', error);
      }
    });
 
    // Load PDF file
    this.fileService.getPdfFile(this.bookId).subscribe({
      next: (blob: Blob) => {
        console.log('PDF Blob:', blob); // Debugging log
        const pdf = new File([blob], 'document.pdf', { type: 'application/pdf' });
        this.files.push({
          id: this.bookId,
          name: 'PDF File',
          file: pdf,
          type: 'pdf',
          url: URL.createObjectURL(pdf)
        });
        console.log('PDF file loaded successfully');
      },
      error: (error: any) => {
        console.error('Error loading PDF file!', error);
      }
    });
  }
 
 
  onFileChange(event: Event, fileType: string) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      const file = input.files[0];
      switch (fileType) {
        case 'audio':
          this.audioFile = file;
          break;
        case 'video':
          this.videoFile = file;
          break;
        case 'pdf':
          this.pdfFile = file;
          break;
      }
    }
  }
 
  onSubmit() {
    const formData = new FormData();
    if (this.audioFile) formData.append('audioFile', this.audioFile, this.audioFile.name);
    if (this.videoFile) formData.append('videoFile', this.videoFile, this.videoFile.name);
    if (this.pdfFile) formData.append('pdfFile', this.pdfFile, this.pdfFile.name);
   
    // Add bookId to formData
    formData.append('bookId', this.bookId.toString());
 
    this.fileService.uploadFiles(formData).subscribe({
      next: (response: any) => {
        console.log('Upload successful!', response);
        this.loadFiles(); // Reload the file list after upload
      },
      error: (error: any) => {
        console.error('Upload failed!', error);
      }
    });
  }
 
 
  deleteFile(fileId: number) {
    this.fileService.deleteFile(fileId).subscribe({
      next: () => {
        console.log('File deleted successfully!');
        this.loadFiles(); // Reload the file list after deletion
      },
      error: (error: any) => {
        console.error('Error deleting file!', error);
      }
    });
  }
}