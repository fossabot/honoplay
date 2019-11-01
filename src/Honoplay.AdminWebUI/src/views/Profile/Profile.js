import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles';
import classNames from 'classnames';
import { Grid, Divider, CircularProgress } from '@material-ui/core';
import Style from '../Style';
import Input from '../../components/Input/InputTextComponent';
import Header from '../../components/Typography/TypographyComponent';
import Button from '../../components/Button/ButtonComponent';
import Departman from './Department';
import Uploader from '../../components/Dropzone/Uploader';
import { connect } from 'react-redux';
import {
  fetchTenant,
  updateTenant
} from '@omegabigdata/honoplay-redux-helper/dist/Src/actions/Tenant';

class Profile extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      tenantModel: {
        name: '',
        description: '',
        logo: '',
        hostName: ''
      },
      loading: false,
      success: false,
      isErrorTenant: false
    };
  }

  tenantId = localStorage.getItem('tenantId');

  componentDidUpdate(prevProps) {
    const {
      isTenantLoading,
      tenant,
      errorTenant,
      isUpdateTenantLoading,
      updatedTenant,
      errorUpdateTenant
    } = this.props;

    if (prevProps.isTenantLoading && !isTenantLoading && tenant) {
      if (!errorTenant) {
        this.setState({
          tenantModel: tenant.items[0]
        });
      }
    }

    if (!prevProps.isUpdateTenantLoading && isUpdateTenantLoading) {
      this.setState({
        loading: true
      });
    }
    if (!prevProps.errorUpdateTenant && errorUpdateTenant) {
      this.setState({
        isErrorTenant: true,
        loading: false,
        success: false
      });
    }
    if (
      prevProps.isUpdateTenantLoading &&
      !isUpdateTenantLoading &&
      updatedTenant
    ) {
      this.props.fetchTenant(this.tenantId.toString());
      if (!errorUpdateTenant) {
        this.setState({
          isErrorTenant: false,
          loading: false,
          success: true
        });
        setTimeout(() => {
          this.setState({ success: false });
        }, 1000);
      }
    }
  }

  componentDidMount() {
    this.props.fetchTenant(this.tenantId.toString());
  }

  handleChange = e => {
    const { name, value } = e.target;
    this.setState(prevState => ({
      tenantModel: {
        ...prevState.tenantModel,
        [name]: value
      },
      isErrorTenant: false
    }));
  };

  handleClick = () => {
    this.props.updateTenant(this.state.tenantModel);
  };

  render() {
    const { tenantModel, loading, success, isErrorTenant } = this.state;
    const { classes } = this.props;
    const buttonClassname = classNames({
      [classes.buttonSuccess]: success
    });

    return (
      <div className={classes.root}>
        <Grid container spacing={3}>
          <Grid item xs={6} sm={11}>
            <Header pageHeader={translate('TenantInformation')} />
          </Grid>
          <Grid item xs={12} sm={1}>
            <Button
              buttonColor="secondary"
              buttonName={translate('Update')}
              onClick={this.handleClick}
              disabled={loading}
              className={buttonClassname}
            />
            {loading && (
              <CircularProgress
                size={24}
                disableShrink={true}
                className={classes.buttonProgressUpdate}
              />
            )}
          </Grid>
          <Grid item xs={12} sm={12} />
          <Grid item xs={12} sm={12}>
            <Divider />
          </Grid>
          <Grid item xs={12} sm={12} />
          <Grid item xs={12} sm={12}>
            <Input
              error={isErrorTenant}
              onChange={this.handleChange}
              labelName={translate('TenantName')}
              inputType="text"
              name="name"
              value={tenantModel && tenantModel.name}
            />
            <Input
              error={isErrorTenant}
              onChange={this.handleChange}
              labelName={translate('Description')}
              multiline
              inputType="text"
              name="description"
              value={tenantModel && tenantModel.description}
            />
          </Grid>
          <Grid item xs={12} sm={12} />
          <Grid item xs={12} sm={2} />
          <Grid item xs={12} sm={9}>
            <Uploader
              selectedImage={value => {
                tenantModel.logo = value;
              }}
              name={value => {}}
              type={value => {}}
            />
          </Grid>
          <Grid item xs={12} sm={12}>
            <Divider />
          </Grid>
          <Grid item xs={12} sm={12} />
          <Grid item xs={12} sm={12}>
            <Departman />
            <Divider />
          </Grid>
        </Grid>
      </div>
    );
  }
}

const mapStateToProps = state => {
  const { isTenantLoading, tenant, errorTenant } = state.tenant;

  const {
    isUpdateTenantLoading,
    updatedTenant,
    errorUpdateTenant
  } = state.updateTenant;

  return {
    isTenantLoading,
    tenant,
    errorTenant,
    isUpdateTenantLoading,
    updatedTenant,
    errorUpdateTenant
  };
};

const mapDispatchToProps = {
  fetchTenant,
  updateTenant
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(withStyles(Style)(Profile));
