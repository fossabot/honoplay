import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { Link } from "react-router-dom";
import { withStyles } from '@material-ui/core/styles';
import { CardHeader, CardMedia, IconButton, Menu, MenuItem } from '@material-ui/core';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import { Style } from './Style';

import { connect } from "react-redux";
import { fetchTenant } from "@omegabigdata/honoplay-redux-helper/dist/Src/actions/Tenant";

class CompanyCard extends React.Component {

  constructor(props) {
    super(props);
    this.state = {
      menu: null,
      tenantModel: {
        name: '',
        description: '',
        logo: '',
        hostName: ''
      }
    };
  }

  tenantId = localStorage.getItem("tenantId");

  componentDidUpdate(prevProps) {
    const {
      isTenantLoading,
      tenant,
      errorTenant
    } = this.props;

    if (prevProps.isTenantLoading && !isTenantLoading && tenant) {
      if (!errorTenant) {
        this.setState({
          tenantModel: tenant.items[0]
        });
      }
    }
  }

  componentDidMount() {
    this.props.fetchTenant(this.tenantId.toString());
  }

  handleClick = (event) => {
    this.setState({ menu: event.currentTarget });
  };

  handleClose = () => {
    this.setState({ menu: null });
  };

  logout = () => {
    localStorage.removeItem('token');
  }

  render() {
    const { menu } = this.state;
    const { classes } = this.props;
    const open = Boolean(menu);

    return (

      <div className={classes.companyCard}>
        <CardHeader
          avatar={
            <CardMedia
              className={classes.cardMedia}
              component="img"
              src={`data:image/jpeg;base64,${this.state.tenantModel.logo}`}
            />
          }
          action={
            <IconButton
              aria-label="More"
              aria-owns={open ? 'table-menu' : undefined}
              aria-haspopup="true"
              onClick={this.handleClick}
            >
              <ExpandMoreIcon />
            </IconButton>
          }
          title={this.state.tenantModel.name}
        />
        <Menu id="table-menu" anchorEl={menu} open={open} onClose={this.handleClose}>
          <MenuItem component={Link} to='/login' onClick={this.logout}>{translate('Logout')}</MenuItem>
        </Menu>
      </div>
    );
  }
}
const mapStateToProps = state => {
  const {
    isTenantLoading,
    tenant,
    errorTenant
  } = state.tenant;

  return {
    isTenantLoading,
    tenant,
    errorTenant
  };
};

const mapDispatchToProps = {
  fetchTenant
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(withStyles(Style)(CompanyCard));