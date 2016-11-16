'use strict'

import { readAsync } from 'fs-jetpack'
import { toHTML } from './markdown/index'

export const CONTENT_CHANGED = 'CONTENT_CHANGED'

const updateContents = (html) => {
  return { type: CONTENT_CHANGED, html }
}

export const convertFileToHTML = (path) => {
  return dispatch => {
    return readAsync(path).then(contents => {
      const md = contents.toString()
      return toHTML(md).then(html => dispatch(updateContents(html)))
    })
  }
}
