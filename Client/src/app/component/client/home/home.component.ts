import { Component, OnDestroy, OnInit, Renderer2 } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent implements OnInit, OnDestroy {
  private addedScripts: HTMLScriptElement[] = [];

  constructor(private renderer: Renderer2) {}

  ngOnInit(): void {
    // Thêm link CSS
    this.addLink('https://fonts.googleapis.com', 'preconnect');
    this.addLink('https://fonts.gstatic.com', 'preconnect', 'crossorigin');
    this.addLink(
      'https://fonts.googleapis.com/css2?family=Jost:wght@300;400;500&family=Lato:wght@300;400;700&display=swap',
      'stylesheet'
    );
    this.addLink(
      'https://cdn.jsdelivr.net/npm/swiper@9/swiper-bundle.min.css',
      'stylesheet'
    );

    // Thêm script JS
    this.addScript('/assets/js/modernizr.js');
    this.addScript('/assets/js/jquery-1.11.0.min.js');
    this.addScript('https://cdn.jsdelivr.net/npm/swiper/swiper-bundle.min.js');
    this.addScript('/assets/js/plugins.js');
    this.addScript('/assets/js/script.js');
  }

  ngOnDestroy(): void {
    // Gỡ bỏ các script được thêm vào khi component bị hủy
    this.addedScripts.forEach((script) => script.remove());
  }

  private addLink(href: string, rel: string, crossorigin?: string): void {
    const link = this.renderer.createElement('link');
    link.rel = rel;
    link.href = href;
    if (crossorigin) link.setAttribute('crossorigin', crossorigin);
    this.renderer.appendChild(document.head, link);
  }

  private addScript(src: string): void {
    const script = this.renderer.createElement('script');
    script.src = src;
    script.type = 'text/javascript';
    this.renderer.appendChild(document.body, script);
    this.addedScripts.push(script); // Lưu lại để gỡ bỏ khi component bị hủy
  }
}
