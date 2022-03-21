import axios from 'axios'
import Vue from 'vue'
import Const from '../static/const.json'

const apiBaseURL = 'https://localhost:44313'

const changeErrorPage = (errorCode: any) => {
  // 認証エラー
  if (errorCode === Const.httpStatusCode.Unauthorized) { location.href = `/error/${errorCode}/errorCode` }
  // 権限なしアラート
  if (errorCode === Const.httpStatusCode.Forbidden) {
    Vue.prototype.$bvModal.msgBoxOk('権限がありません。', {
      title: 'メッセージ',
      size: 'md',
      buttonSize: 'sm',
      headerClass: 'p-2 border-bottom-0',
      footerClass: 'p-2 border-top-0',
      centered: true
    })
  }
}

export default {
  get<T> (url: any, options = {}): Promise<T> {
    return axios({
      method: 'GET',
      url: `${apiBaseURL}${url}`,
      ...options
    })
      .then(res => res.data)
      .catch((err) => {
        changeErrorPage(err.response.status)
      })
  },
  post<T> (url: any, options = {}): Promise<T> {
    return axios({
      method: 'POST',
      url: `${apiBaseURL}${url}`,
      ...options
    })
      .then(res => res.data)
      .catch((err) => {
        changeErrorPage(err.response.status)
      })
  },
  postHeader<T> (url: any, data: any): Promise<T> {
    return axios({
      method: 'POST',
      url: `${apiBaseURL}${url}`,
      data,
      headers: { 'Content-Type': 'application/json; charset=utf-8' }
    })
      .then(res => res.data)
      .catch((err) => {
        changeErrorPage(err.response.status)
      })
  },
  put<T> (url: any, options = {}): Promise<T> {
    return axios({
      method: 'PUT',
      url: `${apiBaseURL}${url}`,
      ...options
    })
      .then(res => res.data)
      .catch((err) => {
        changeErrorPage(err.response.status)
      })
  },
  putHeader<T> (url: any, data: any): Promise<T> {
    return axios({
      method: 'PUT',
      url: `${apiBaseURL}${url}`,
      data,
      headers: { 'Content-Type': 'application/json; charset=utf-8' }
    })
      .then(res => res.data)
      .catch((err) => {
        changeErrorPage(err.response.status)
      })
  },
  delete (url: any, options = {}): Promise<void> {
    return axios({
      method: 'DELETE',
      url: `${apiBaseURL}${url}`,
      ...options
    })
      .then(res => res.data)
      .catch((err) => {
        changeErrorPage(err.response.status)
      })
  },
  deleteHeader (url: any, data: any): Promise<void> {
    return axios({
      method: 'DELETE',
      url: `${apiBaseURL}${url}`,
      data,
      headers: { 'Content-Type': 'application/json; charset=utf-8' }
    })
      .then(res => res.data)
      .catch((err) => {
        changeErrorPage(err.response.status)
      })
  }
}
