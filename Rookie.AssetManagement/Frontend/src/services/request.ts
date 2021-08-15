import axios, { AxiosInstance, AxiosRequestConfig } from "axios";
import { UrlBackEnd } from "../constants/oidc-config";

const config: AxiosRequestConfig = {
    baseURL: UrlBackEnd,
}

class RequestService {
    public axios: AxiosInstance;

    constructor() {
        console.log(config.baseURL);
        this.axios = axios.create(config);
        //this.axios.defaults.headers.common['Content-Type'] = `application/json`;
    }

    public setAuthentication(accessToken: string) {
        this.axios.defaults.headers.common['Authorization'] = `Bearer ${accessToken}`;
    }
}

export default new RequestService();
