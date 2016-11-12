'use strict'

import { CONTENT_CHANGED } from './actions'

export default (state, action) => {
  if (!state) state = { html: '', updatedAt: null }

  switch (action.type) {
    case CONTENT_CHANGED:
      return { html: action.html, updatedAt: Date.now() }
    default:
      return state
  }
}
