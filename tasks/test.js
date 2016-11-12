const gulp = require('gulp')
const mocha = require('gulp-mocha')

gulp.task('test', ['build'], () => {
  gulp
    .src('spec/**/*.js', { read: false })
    .pipe(mocha({ reporter: 'spec', require: './spec/helper' }))
    .once('error', () => { process.exit(1) })
    .once('end', () => { process.exit() })
})
