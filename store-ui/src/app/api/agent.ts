import axios, { AxiosError, AxiosResponse } from "axios";
import { toast } from "react-toastify";
import { router } from "../router/Routes";

axios.defaults.baseURL = 'http://localhost:5000/api/';

const sleep = () => new Promise(resolve => setTimeout(resolve, 500));

axios.interceptors.response.use(async response => {
    await sleep();
    return response
}, (error: AxiosError) => {
    const { data, status } = error.response as AxiosResponse;
    switch (status) {
        case 400:
            if (data.errors) {
                const modelStateError: string[] = [];
                for (const key in data.errors) {
                    if (data.errors[key])
                        modelStateError.push(data.errors[key]);
                }
                throw modelStateError.flat();
            }
            toast.error(data.title);
            break;
        case 401: toast.error(data.title); break;
        case 500:
            router.navigate('/server-error', { state: { error: data } });
            break;
        default: break;
    }

    console.log('Caught by Axios Interceptor');
    return Promise.reject(error.response);
})

const responseBody = (response: AxiosResponse) => response.data;

const requests = {
    get: (url: string) => axios.get(url).then(responseBody),
    post: (url: string, body: {}) => axios.post(url).then(responseBody),
    put: (url: string, body: {}) => axios.put(url).then(responseBody),
    delete: (url: string) => axios.delete(url).then(responseBody)
}

const Catalog = {
    list: () => requests.get('Products'),
    details: (id: number) => requests.get(`Products/${id}`)
}

const TestErrors = {
    get400Error: () => requests.get('/Error/bad-request'),
    get401Error: () => requests.get('/Error/unauthorized'),
    get404Error: () => requests.get('/Error/not-found'),
    get500Error: () => requests.get('/Error/server-error'),
    getValidationError: () => requests.get('/Error/validation-error')
}
const agent = {
    Catalog,
    TestErrors
}

export default agent;