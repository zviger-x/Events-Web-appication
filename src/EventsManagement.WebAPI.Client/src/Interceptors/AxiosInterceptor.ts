import axios, { AxiosResponse } from "axios";

let isInterceptorSetup = false;

export const SetupErrorHandlingInterceptor = () => {
    if (!isInterceptorSetup) {
        axios.interceptors.response.use(
            (response: AxiosResponse) => response,
            (error) => {
                if (error.response) {
                    const statusCode = error.response.status;
                    const data = error.response.data;

                    switch (statusCode) {
                        case 400:
                            if (data) {
                                console.log(data);
                            }
                            break;
                        case 401:
                            console.log("Unauthorized");
                            break;
                        case 403:
                            console.log("Forbidden");
                            break;
                        case 404:
                            console.log("Not found");
                            break;
                        default:
                            console.log("Generic error");
                    }
                }

                return Promise.reject(error);
            }
        )

        isInterceptorSetup = true;
    }
}