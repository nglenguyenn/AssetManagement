import React, { lazy, Suspense, useEffect } from "react";
import { Route, Switch } from "react-router-dom";

import { CHANGEPASSWORD, FIRSTTIMECHANGEPASSWORD, HOME, LOGIN } from '../constants/pages';
import InLineLoader from "../components/InlineLoader";
import { useAppDispatch, useAppSelector } from "../hooks/redux";
import LayoutRoute from "./LayoutRoute";
import Roles from "src/constants/roles";
import { changePassword, me } from "src/containers/Authorize/reducer";
import Layout from "src/containers/Layout";

const Home = lazy(() => import('../containers/Home'));
const Login = lazy(() => import('../containers/Authorize'));
const NotFound = lazy(() => import("../containers/NotFound"));
const ChangePassword = lazy (() => import("../containers/ChangePassword"));
const FirstTimeChangePassword = lazy (() => import("../containers/FirstTimeChangePassword"));

const SusspenseLoading = ({ children }) => (
  <Suspense fallback={<InLineLoader />}>
    {children}
  </Suspense>
);

const Routes = () => {
  const { isAuth, account } = useAppSelector(state => state.authReducer);
  const dispatch = useAppDispatch();

  useEffect(() => {
    dispatch(me());
  }, []);

  return (
    <SusspenseLoading>
      <Switch>
        <Route exact path={LOGIN} component={Login} />
        <LayoutRoute exact path={HOME}>
          <Home />
        </LayoutRoute>
        <Route exact path={CHANGEPASSWORD} component={ChangePassword}/>
        <Route exact path= {FIRSTTIMECHANGEPASSWORD} component={FirstTimeChangePassword}/>
      </Switch>
    </SusspenseLoading>
  )
};

export default Routes;
