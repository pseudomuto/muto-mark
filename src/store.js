'use strict'

import { createStore, applyMiddleware } from 'redux'
import { default as thunk } from 'redux-thunk'
import reducer from './reducer'

export default (initialState) => createStore(reducer, initialState, applyMiddleware(thunk))
