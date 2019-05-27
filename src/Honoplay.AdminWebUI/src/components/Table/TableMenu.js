import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles';
import {IconButton, Menu, MenuItem } from '@material-ui/core';
import MoreVertIcon from '@material-ui/icons/MoreHoriz';
import { Style } from './Style';

class TableMenu extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      menu: null,
    };
    this.handleClick=this.handleClick.bind(this);
    this.handleClose=this.handleClose.bind(this);
  }

  handleClick (event) {
    this.setState({ menu: event.currentTarget });
  };
  
  handleClose ()  {
    this.setState({ menu: null });
  };

  render() {
    const { menu } = this.state;
    const { classes, handleDelete } = this.props;
    const open = Boolean(menu);

    return (
      <div>
        <IconButton
          aria-label="More"
          aria-owns={open ? 'table-menu' : undefined}
          aria-haspopup="true"
          onClick={this.handleClick}
        >
          <MoreVertIcon className={classes.tableMenu}/>
        </IconButton>
        <Menu id="table-menu" anchorEl={menu} open={open} onClose={this.handleClose}>
              <MenuItem onClick={this.handleClose}>{translate('Edit')}</MenuItem>
              <MenuItem onClick={handleDelete}>{translate('Remove')}</MenuItem>
        </Menu>
      </div>
    );
  }
}

export default withStyles(Style)(TableMenu);