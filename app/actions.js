'use strict'

const fs = require('fs')
const markdown = require('./markdown')

const CONTENT_CHANGED = 'CONTENT_CHANGED'

const updateContents = (html) => {
  return { type: CONTENT_CHANGED, html: html }
}

const readFile = (path) => {
  return new Promise((resolve, reject) => {
    fs.readFile(path, (error, content) => {
      if (error) {
        reject(error)
      } else {
        resolve(content)
      }
    })
  })
}

const convertFileToHTML = (path) => {
  return dispatch => {
    return readFile(path).then(contents => {
      let md = contents.toString()
      return markdown.toHTML(md).then(html => dispatch(updateContents(html)))
    })
  }
}

module.exports = {
  CONTENT_CHANGED,
  convertFileToHTML
}
