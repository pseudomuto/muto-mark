const childProcess = require('child_process')
const electron = require('electron')
const gulp = require('gulp')

gulp.task('run', ['build'], () => {
  childProcess.spawn(electron, ['.'], { stdio: 'inherit' }).on('close', () => {
    process.exit()
  })
})
