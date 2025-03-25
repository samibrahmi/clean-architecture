import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';
import fs from 'fs';
import path from 'path';

// Get port from command line arguments or default to 3000
const defaultPort = 3000;
const port = parseInt(process.env.PORT || defaultPort, 10);

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [
    react({
      // Include JSX in .js files
      include: "**/*.{jsx,js,tsx,ts}",
    }),
  ],
  server: {
    port: port, // Use configurable port
    host: '0.0.0.0', // Allow connections from outside localhost
    strictPort: false, // Allow Vite to use another port if default is in use
    open: true,
  },
  build: {
    outDir: 'build', // Match CRA output directory
  },
  resolve: {
    extensions: ['.js', '.jsx', '.ts', '.tsx']
  },
});
