import tailwindcss from '@tailwindcss/vite';
import plugin from '@vitejs/plugin-react';
import { defineConfig } from 'vite';

export default defineConfig({
  plugins: [plugin(), tailwindcss()],
  server: {
    port: 59752,
  },
});
