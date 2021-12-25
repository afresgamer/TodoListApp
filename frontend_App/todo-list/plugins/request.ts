import axios from 'axios'

const apiBaseURL = 'https://localhost:44313'

export default {
  get<T> (url: any, options = {}): Promise<T> {
    return axios({
      method: 'GET',
      url: `${apiBaseURL}${url}`,
      ...options
    })
      .then(res => res.data)
      .catch()
  },
  post<T> (url: any, options = {}): Promise<T> {
    return axios({
      method: 'POST',
      url: `${apiBaseURL}${url}`,
      ...options
    })
      .then(res => res.data)
      .catch()
  },
  postHeader<T> (url: any, data: any): Promise<T> {
    return axios({
      method: 'POST',
      url: `${apiBaseURL}${url}`,
      data,
      headers: { 'Content-Type': 'application/json; charset=utf-8' }
    })
      .then(res => res.data)
      .catch()
  },
  put<T> (url: any, options = {}): Promise<T> {
    return axios({
      method: 'PUT',
      url: `${apiBaseURL}${url}`,
      ...options
    })
      .then(res => res.data)
      .catch()
  },
  putHeader<T> (url: any, data: any): Promise<T> {
    return axios({
      method: 'PUT',
      url: `${apiBaseURL}${url}`,
      data,
      headers: { 'Content-Type': 'application/json; charset=utf-8' }
    })
      .then(res => res.data)
      .catch()
  },
  delete (url: any, options = {}): Promise<void> {
    return axios({
      method: 'DELETE',
      url: `${apiBaseURL}${url}`,
      ...options
    })
      .then(res => res.data)
      .catch()
  },
  deleteHeader (url: any, data: any): Promise<void> {
    return axios({
      method: 'DELETE',
      url: `${apiBaseURL}${url}`,
      data,
      headers: { 'Content-Type': 'application/json; charset=utf-8' }
    })
      .then(res => res.data)
      .catch()
  }
}
