import jwtDecode from "jwt-decode";

import { LOCAL_STORAGE_KEY_AUTHENTICATION_TOKEN } from "../Constants";

export function isLoggedIn(): boolean {
    return (getLoginToken() !== null);
}

export function getLoginToken(): (string | null) {
    return localStorage.getItem(LOCAL_STORAGE_KEY_AUTHENTICATION_TOKEN);
}

export function setLoginToken(token: string) {
    localStorage.setItem(
        LOCAL_STORAGE_KEY_AUTHENTICATION_TOKEN,
        token
    );
}

export function clearLoginToken() {
    localStorage.removeItem(LOCAL_STORAGE_KEY_AUTHENTICATION_TOKEN);
}

interface LoginToken {
    unique_name: string;
    exp: number;
    iat: number;
    nbf: number;
}

export function getUserId(): (number | null) {
    const jwtToken = getLoginToken();
    if (jwtToken) {
        const decodedToken: LoginToken = jwtDecode(jwtToken);
        return parseInt(decodedToken.unique_name);
    }
    return null;
}
