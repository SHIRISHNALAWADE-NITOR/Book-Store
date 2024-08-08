const { defineConfig } = require('@vue/cli-service')

module.exports = defineConfig({
  transpileDependencies: true,
  devServer: {
    https: false, // Ensure HTTPS is not enabled here for HTTP usage
    port: 8080,
    proxy: {
      '/api': {
        target: 'http://localhost:5134', // Ensure the target is correct for HTTP
        changeOrigin: true,
        pathRewrite: { '^/api': '' },
      },
    },
  },
})
