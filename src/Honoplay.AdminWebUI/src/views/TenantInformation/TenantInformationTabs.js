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
import TabContainer from "../../components/Tabs/TabContainer";
import Typography from "../../components/Typography/TypographyComponent";
import MySnackbarContentWrapper from "../../components/Snackbar/SnackbarContextComponent";

import BasicKnowledge from "./BasicKnowledge";
import Department from "./Department";
import Trainee from "./Trainee";

import { connect } from "react-redux";
import { createTenant } from "@omegabigdata/honoplay-redux-helper/dist/Src/actions/Tenant";
import { createTrainee, fetchTraineeList } from "@omegabigdata/honoplay-redux-helper/dist/Src/actions/Trainee";
import { fetchDepartmentList } from "@omegabigdata/honoplay-redux-helper/dist/Src/actions/Department";

class TenantInformationTabs extends React.Component {
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
      isErrorTenant: false,
      isErrorTrainee: false,
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

    const { tabValue } = this.state;

    if (tabValue == 0) {
      if (!prevProps.errorCreateTenant && errorCreateTenant) {
        this.setState({
          loading: false,
          isErrorTenant: true,
          success: false,
          open: true
        });
      }
      if (prevProps.isCreateTenantLoading && !isCreateTenantLoading && newTenant) {
        if (!errorCreateTenant)
        {
          this.setState({
            loading: false,
            success: true,
            isErrorTenant: false,
            open: true,
            disabledTab1: false,
            disabledTab2: false,
          });
        }
      }
      if (!prevProps.isCreateTenantLoading && isCreateTenantLoading) {
        if (!errorCreateTenant) {
          this.setState({
            loading: true,
            success: false,
            open: errorCreateTenant && true,
            isErrorTenant: errorCreateTenant && true,
          });
        }
      } else if (prevProps.isCreateTenantLoading && !isCreateTenantLoading) {
        if (!errorCreateTenant)
        {
          setTimeout(() => {
            this.setState({
              loading: false,
              success: false,
              open: true,
              isErrorTenant: errorCreateTenant && true,
            });
          }, 500);
        }
      }
    }
    if (tabValue == 2) {
      if (!prevProps.errorCreateTrainee && errorCreateTrainee) {
        this.setState({
          isErrorTrainee: true,
          open: true
        });
      }
      if (prevProps.isCreateTraineeLoading && !isCreateTraineeLoading && newTrainee) {
        this.props.fetchTraineeList(0, 50);
        this.setState({
          loading: false,
          success: true,
          isErrorTrainee: false,
          open: true,
        });
      }
      if (!prevProps.isCreateTraineeLoading && isCreateTraineeLoading) {
        this.setState({
          loading: true,
          success: false,
          open: errorCreateTrainee && true,
          isErrorTrainee: errorCreateTrainee && true,
        });
      } else if (prevProps.isCreateTraineeLoading && !isCreateTraineeLoading) {
        this.setState({
          loading: false,
          success: false,
          open: true,
          isErrorTrainee: errorCreateTrainee && true,
        });
      }
    }

  }
  handleButtonClick = () => {
    const { createTenant, createTrainee } = this.props;
    const { tabValue, newTenantModel, newTraineeModel } = this.state;

    if (tabValue == 0) {
      createTenant(newTenantModel);
    }
    if (tabValue == 2) {
      createTrainee(newTraineeModel);
    }
  };

  handleChange = (event, tabValue) => {
    this.setState({ tabValue });
  };

  handleChangeIndex = (index) => {
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
      isErrorTrainee,
      isErrorTenant
    } = this.state;
    const { classes } = this.props;
    const buttonClassname = classNames({
      [classes.buttonSuccess]: success
    });

    return (
      
      <MuiThemeProvider theme={theme}>
        <div className={classes.root}>
          <Grid container spacing={3}>
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
                variant={isErrorTrainee || isErrorTenant ? "error" : "success"}
                message={
                  isErrorTrainee || isErrorTenant
                    ? translate('OperationFailed!')
                    : translate('YourTransactionSuccessful')
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
                index={tabValue}
                onChangeIndex={this.handleChangeIndex}
              >
                <TabContainer dir={theme.direction}>
                  <BasicKnowledge
                    basicTenantModel={model => {
                      if (model) {
                        this.setState({
                          newTenantModel: model,
                          isErrorTenant: false
                        });
                      }
                    }}
                    isErrorTenant={isErrorTenant}
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
                          isErrorTrainee: false
                        })
                      }
                    }}
                    isErrorTrainee={isErrorTrainee}
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
  const {
    errorCreateTenant,
    isCreateTenantLoading,
    newTenant
  } = state.createTenant;
  const {
    errorCreateTrainee,
    isCreateTraineeLoading,
    newTrainee
  } = state.createTrainee;

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
  createTrainee,
  fetchDepartmentList,
  fetchTraineeList
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(withStyles(Style)(TenantInformationTabs));
