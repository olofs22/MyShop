import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

export default defineConfig({
  plugins: [react()],
  base: '/MyShop/',   // <-- change "MyShop" to your exact GitHub repo name
})