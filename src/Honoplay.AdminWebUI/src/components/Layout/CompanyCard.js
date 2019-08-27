import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { Link } from "react-router-dom";
import { withStyles } from '@material-ui/core/styles';
import { CardHeader, CardMedia, IconButton, Menu, MenuItem } from '@material-ui/core';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import { Style } from './Style';

import imageUrl from '../../images/deneme.jpg';

class CompanyCard extends React.Component {

  constructor(props) {
    super(props);
    this.state = {
      menu: null,
    };
  }

  handleClick = (event) => {
    this.setState({ menu: event.currentTarget });
  };

  handleClose = () => {
    this.setState({ menu: null });
  };

  logout = () => {
    localStorage.removeItem('token')
  }

  render() {
    const { menu } = this.state;
    const { classes, companyName } = this.props;
    const open = Boolean(menu);
    return (

      <div className={classes.companyCard}>
        <CardHeader
          avatar={
            <CardMedia
              className={classes.cardMedia}
              component="img"
              src={imageUrl}
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
          title={companyName}
        />
        <Menu id="table-menu" anchorEl={menu} open={open} onClose={this.handleClose}>
          <MenuItem component={Link} to='/login' onClick={this.logout}>{translate('Logout')}</MenuItem>
        </Menu>
      </div>
    );
  }
}

export default withStyles(Style)(CompanyCard);