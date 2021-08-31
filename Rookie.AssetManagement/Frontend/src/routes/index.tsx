import React, { lazy, Suspense, useEffect } from "react";
import { Route, Switch } from "react-router-dom";
import { ASSET, ASSIGNMENT, CHANGE_PASSWORD, HOME, LOGIN, USER } from "../constants/pages";
import InLineLoader from "../components/InlineLoader";
import { useAppDispatch, useAppSelector } from "../hooks/redux";
import LayoutRoute from "./LayoutRoute";
import PrivateRoute from "./PrivateRoute";
import Roles from "src/constants/roles";
import { changePassword, me } from "src/containers/Authorize/reducer";
import Layout from "src/containers/Layout";
import EditUserContainer from "src/containers/User/Edit";

const Home = lazy(() => import("../containers/Home"));
const Login = lazy(() => import("../containers/Authorize"));
const User = lazy(() => import("../containers/User"));
const Asset = lazy(() => import("../containers/Asset"));
const Assignment = lazy(()=> import("../containers/Assignment"))
const ChangePassword = lazy(() => import("../containers/ChangePassword"));
const FirstTimeChangePassword = lazy(
  () => import("../containers/FirstTimeChangePassword")
);

const SusspenseLoading = ({ children }) => (
  <Suspense fallback={<InLineLoader />}>{children}</Suspense>
);

const Routes = () => {
  const { isAuth, account } = useAppSelector((state) => state.authReducer);
  const dispatch = useAppDispatch();

  useEffect(() => {
    dispatch(me());
  }, []);

  return (
    <SusspenseLoading>
      <Switch>
        <Route exact path={LOGIN} component={Login} />
        
        <PrivateRoute exact path={HOME}>
          <Home />
        </PrivateRoute>

        <PrivateRoute path={USER}>
          <User />
        </PrivateRoute>

        <PrivateRoute path={ASSET}>
          <Asset />
        </PrivateRoute>
        
        <PrivateRoute path={ASSIGNMENT}>
          <Assignment />
        </PrivateRoute>

        <Route exact path={CHANGE_PASSWORD} component={ChangePassword} />
      </Switch>
    </SusspenseLoading>
  );
};

export default Routes;
