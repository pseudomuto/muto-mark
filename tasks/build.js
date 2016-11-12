const gulp = require('gulp')
const rollup = require('./rollup')

gulp.task('build', () => {
  return Promise.all([
    rollup('./src/main.js', './app/main.js'),
    rollup('./src/ui/browser.js', './app/browser.js')
  ])
})
