import { NOTFOUND } from "dns";
import React, { lazy, Suspense } from "react";
import { useEffect } from "react";
import { Redirect, Route } from "react-router-dom";

import { ERROR, LOGIN } from "src/constants/pages";
import Layout from "src/containers/Layout";
import { useAppSelector } from "src/hooks/redux";
import { getLocalStorage } from "src/utils/localStorage";
import InLineLoader from "../components/InlineLoader";


const FirstTimeChangePassword = lazy(() => import("../containers/FirstTimeChangePassword"));

export default function PrivateRoute({ children, ...rest }) {
    const { isAuth, account } = useAppSelector(state => state.authReducer);
    return (
        <Route
            {...rest}
            render={({ location }) =>
                isAuth ? (
                    (account?.status.localeCompare("Success") == 0) ?
                        (<Suspense fallback={<InLineLoader />}>
                            <Layout>
                                {children}
                            </Layout>
                        </Suspense>)
                        : ((account?.status.localeCompare("NeedChangePassword") == 0) ?
                            (<Suspense fallback={<InLineLoader />}>
                                <Layout>
                                    {children}
                                    <FirstTimeChangePassword />
                                </Layout>
                            </Suspense>)
                            : <Redirect to={NOTFOUND} />
                        )) : <Redirect to={LOGIN} />}
        />
    );
}