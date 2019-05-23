import React from 'react';
import { EDIT, REMOVE } from '../../helpers/TerasuKey';
import { withStyles } from '@material-ui/core/styles';
import {IconButton, Menu, MenuItem } from '@material-ui/core';
import MoreVertIcon from '@material-ui/icons/MoreHoriz';
import { Style } from './Style';

class TableMenu extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      anchorEl: null,
    };
    this.handleClick=this.handleClick.bind(this);
    this.handleClose=this.handleClose.bind(this);
  }

  handleClick (event) {
    this.setState({ anchorEl: event.currentTarget });
  };
  
  handleClose ()  {
    this.setState({ anchorEl: null });
  };

  render() {
    const { anchorEl } = this.state;
    const { classes, handleDelete } = this.props;
    const open = Boolean(anchorEl);

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
        <Menu id="table-menu" anchorEl={anchorEl} open={open} onClose={this.handleClose}>
              <MenuItem onClick={this.handleClose}>{EDIT}</MenuItem>
              <MenuItem onClick={handleDelete}>{REMOVE}</MenuItem>
        </Menu>
      </div>
    );
  }
}

export default withStyles(Style)(TableMenu);