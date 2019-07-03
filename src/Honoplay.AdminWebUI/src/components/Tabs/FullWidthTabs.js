import React from "react";
import { translate } from "@omegabigdata/terasu-api-proxy";
import { withStyles } from "@material-ui/core/styles";
import classNames from "classnames";
import SwipeableViews from "react-swipeable-views";
import {
  Tabs,
  Tab,
  MuiThemeProvider,
  Grid,
  CircularProgress,
  Button,
  Snackbar
} from "@material-ui/core";
import { Style, theme } from "./Style";
import TabContainer from "./TabContainer";
import Typography from "../../components/Typography/TypographyComponent";

import MySnackbarContentWrapper from "./SnackbarContextComponent";

import BasicKnowledge from "../../views/TenantInformation/BasicKnowledge";
import Department from "../../views/TenantInformation/Department";
import Trainee from "../../views/TenantInformation/Trainee";

import { connect } from "react-redux";
import { createTenant } from "@omegabigdata/honoplay-redux-helper/Src/actions/Tenant";
import { createTrainee } from "@omegabigdata/honoplay-redux-helper/Src/actions/Trainee";

class FullWidthTabs extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      tabValue: 0,
      loading: false,
      success: false,
      disabledTab1: true,
      disabledTab2: true,
      open: false,
      newTenantModel: null,
      isError: false,
      newTraineeModel: null
    };
  }

  componentDidUpdate(prevProps) {
    const { 
      isCreateTenantLoading,
      errorCreateTenant,
      newTenant,
      errorCreateTrainee,
      isCreateTraineeLoading,
      newTrainee
    } = this.props;

    if (this.state.tabValue == 0) {
      if (!prevProps.errorCreateTenant && errorCreateTenant) {
        this.setState({
          isError: true,
          open: true
        });
      }
      if (prevProps.isCreateTenantLoading && !isCreateTenantLoading && newTenant) {
        this.setState({
          loading: false,
          success: true,
          isError: false,
          open: true,
          disabledTab1: false,
          disabledTab2: false,
        });
      }
      if (!prevProps.isCreateTenantLoading && isCreateTenantLoading) {
        this.setState({
          loading: true,
          success: false,
          open: errorCreateTenant && true,
          isError: errorCreateTenant && true,
        });
      } else if (prevProps.isCreateTenantLoading && !isCreateTenantLoading) {
        this.setState({
          loading: false,
          success: false,
          open: true,
          isError: errorCreateTenant && true,
        });
      }
    }

    if (this.state.tabValue == 2) {
      if (!prevProps.errorCreateTrainee && errorCreateTrainee) {
        this.setState({
          isError: true,
          open: true
        });
      }
      if (prevProps.isCreateTraineeLoading && !isCreateTraineeLoading && newTrainee) {
        this.setState({
          loading: false,
          success: true,
          isError: false,
          open: true,
        });
      }
      if (!prevProps.isCreateTraineeLoading && isCreateTraineeLoading) {
        this.setState({
          loading: true,
          success: false,
          open: errorCreateTrainee && true,
          isError: errorCreateTrainee && true,
        });
      } else if (prevProps.isCreateTraineeLoading && !isCreateTraineeLoading) {
        this.setState({
          loading: false,
          success: false,
          open: true,
          isError: errorCreateTrainee && true,
        });
      }
    }

  }
  handleButtonClick = () => {
    const { createTenant, createTrainee } = this.props;
    createTenant(this.state.newTenantModel);
    createTrainee(this.state.newTraineeModel);
    console.log(this.state.newTraineeModel);
  };

  handleChange = (event, tabValue) => {
    this.setState({ tabValue });
  };

  handleChangeIndex = index => {
    this.setState({ tabValue: index });
  };

  handleClose = (reason) => {
    if (reason === "clickaway") {
      return;
    }
    this.setState({ open: false });
  };

  render() {
    const {
      loading,
      success,
      tabValue,
      disabledTab1,
      disabledTab2,
      open,
      isError
    } = this.state;
    const { classes } = this.props;
    const buttonClassname = classNames({
      [classes.buttonSuccess]: success
    });
    return (
      <MuiThemeProvider theme={theme}>
        <div className={classes.root}>
          <Grid container spacing={24}>
            <Grid item xs={6} sm={10}>
              <Typography pageHeader={translate("TenantInformation")} />
            </Grid>
            <Grid item xs={6} sm={2}>
              <div className={classes.buttonRoot}>
                <div className={classes.buttonWrapper}>
                  <Button
                    variant="contained"
                    color="primary"
                    className={buttonClassname}
                    disabled={loading}
                    onClick={this.handleButtonClick}
                  >
                    {translate("Save")}
                  </Button>
                  {loading && (
                    <CircularProgress
                      size={24}
                      className={classes.buttonProgress}
                    />
                  )}
                </div>
              </div>
            </Grid>
            <Snackbar
              anchorOrigin={{
                vertical: "bottom",
                horizontal: "left"
              }}
              autoHideDuration={1500}
              open={open}
              onClose={this.handleClose}
            >
              <MySnackbarContentWrapper
                onClose={this.handleClose}
                variant={isError ? "error" : "success"}
                message={
                  isError
                    ? "İşlem başarısız"
                    : "İşleminiz başarılı bir şekilde kaydedildi!"
                }
              />
            </Snackbar>
            <Grid item xs={12} sm={12}>
              <Tabs
                indicatorColor="secondary"
                value={tabValue}
                onChange={this.handleChange}
                textColor="primary"
                variant="fullWidth"
              >
                <Tab
                  className={
                    tabValue === 0
                      ? classes.active_tab
                      : classes.default_tabStyle
                  }
                  label={translate("BasicKnowledge")}
                />
                <Tab
                  className={
                    tabValue === 1
                      ? classes.active_tab
                      : classes.default_tabStyle
                  }
                  label={translate("Department")}
                  disabled={disabledTab1}
                />
                <Tab
                  className={
                    tabValue === 2
                      ? classes.active_tab
                      : classes.default_tabStyle
                  }
                  label={translate("Trainees")}
                  disabled={disabledTab2}
                />
              </Tabs>
              <SwipeableViews
                axis={theme.direction === "rtl" ? "x-reverse" : "x"}
                index={this.state.tabValue}
                onChangeIndex={this.handleChangeIndex}
              >
                <TabContainer dir={theme.direction}>
                  <BasicKnowledge
                    basicTenantModel={model => {
                      if (model) {
                        this.setState({
                          newTenantModel: model,
                          isError: false
                        });
                      }
                    }}
                    isError={isError}
                  />
                </TabContainer>
                <TabContainer dir={theme.direction}>
                  <Department />
                </TabContainer>
                <TabContainer dir={theme.direction}>
                  <Trainee
                    basicTraineeModel={model => {
                      if (model) {
                        this.setState({
                          newTraineeModel: model,
                          isError: false
                        })
                      }
                    }}
                    isError={isError}
                  />
                </TabContainer>
              </SwipeableViews>
            </Grid>
          </Grid>
        </div>
      </MuiThemeProvider>
    );
  }
}

const mapStateToProps = state => {
  const { errorCreateTenant, isCreateTenantLoading, newTenant } = state.createTenant;
  const { errorCreateTrainee, isCreateTraineeLoading, newTrainee } = state.createTrainee;
  return {
    errorCreateTenant,
    isCreateTenantLoading,
    newTenant,
    errorCreateTrainee,
    isCreateTraineeLoading,
    newTrainee
  };
};

const mapDispatchToProps = {
  createTenant,
  createTrainee
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(withStyles(Style)(FullWidthTabs));
