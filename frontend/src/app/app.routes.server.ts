import { RenderMode, ServerRoute } from '@angular/ssr';

export const serverRoutes: ServerRoute[] = [

  {
    path: 'about',
    renderMode: RenderMode.Prerender
  },
  {
    path: 'contact',
    renderMode: RenderMode.Prerender
  },

  {
    path: 'details/:slug/:id',
    renderMode: RenderMode.Server
  },

  {
    path: 'payment/:cartId',
    renderMode: RenderMode.Server
  },

  {
    path: '**',
    renderMode: RenderMode.Server
  },


];
