import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles';
import Edit from '@material-ui/icons/Edit';
import {
  Table,
  TableBody,
  TableCell,
  TablePagination,
  TableRow,
  Paper,
  Checkbox,
  MuiThemeProvider,
  IconButton,
} from '@material-ui/core';
import { Style, theme } from './Style';

import EnhancedTableHead from './EnhancedTableHead';
import EnhancedTableToolbar from './EnhancedTableToolbar';
import Modal from '../Modal/Modal';

class TableComponent extends React.Component {

  constructor(props) {
    super(props);
    this.state = {
      openDialog: false,
      selected: [],
      page: 0,
      rowsPerPage: 5,
    };
  }

  handleClickOpenDialog = () => {
    this.setState({ openDialog: true });
  };

  handleCloseDialog = () => {
    this.setState({ openDialog: false });
  };

  handleSelectAllClick = (event) => {
    const { data } = this.props;
    if (event.target.checked) {
      this.setState({
        selected: data.map(n => n.id)
      })
      return;
    }
    this.setState({ selected: [] });
  };

  handleClick = (id) => {
    const { selected } = this.state;
    const selectedIndex = selected.indexOf(id);
    let newSelected = [];

    if (selectedIndex === -1) {
      newSelected = newSelected.concat(selected, id);
    } else if (selectedIndex === 0) {
      newSelected = newSelected.concat(selected.slice(1));
    } else if (selectedIndex === selected.length - 1) {
      newSelected = newSelected.concat(selected.slice(0, -1));
    } else if (selectedIndex > 0) {
      newSelected = newSelected.concat(
        selected.slice(0, selectedIndex),
        selected.slice(selectedIndex + 1),
      );
    }
    this.setState({ selected: newSelected });
  };

  handleChangePage = (event, page) => {
    this.setState({ page });
  };

  handleChangeRowsPerPage = (event) => {
    const { data } = this.props;
    if (data.length > 0) {
      this.setState({
        page: 0,
        rowsPerPage: event.target.value
      });
    }
  };

  isSelected = (id) => {
    const { selected } = this.state;
    return selected.indexOf(id) !== -1;
  }

  handleDelete = (index) => {
    const { data } = this.props;
    const id = data.map(row => row.id);
    const tableData = [...data];
    index.map(k => {
      let n = id.indexOf(k);
      id.splice(n, 1);
      data.splice(n, 1);
    });
    this.setState({ tableData });
    this.setState({ selected: [] });
  };

  render() {
    const {
      classes,
      data,
      columns,
      children
    } = this.props;
    const {
      selected,
      rowsPerPage,
      page,
      openDialog
    } = this.state;
    const emptyRows = rowsPerPage - Math.min(rowsPerPage, data.length - page * rowsPerPage);
    return (

      <MuiThemeProvider theme={theme} >
        <Paper
          classes={{
            root: classes.tableRoot,
            typography: classes.typography
          }}>
          <EnhancedTableToolbar
            numSelected={selected.length}
            handleDelete={() => this.handleDelete(selected)}
          />
          <div
            className={classes.tableWrapper}>
            <Table
              className={classes.table}
              size="small"
            >
              <EnhancedTableHead
                numSelected={selected.length}
                onSelectAllClick={this.handleSelectAllClick}
                rowCount={data.length}
                columns={columns}
              />
              <TableBody>
                {data
                  .slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                  .map((data, id) => {
                    const isSelected = this.isSelected(data.id);
                    return (

                      <TableRow
                        hover
                        onClick={() => this.handleClick(data.id)}
                        role="checkbox"
                        aria-checked={isSelected}
                        tabIndex={-1}
                        selected={isSelected}
                        key={id}
                      >
                        <TableCell
                          padding="checkbox"
                          className={classes.tableCell}>
                          <Checkbox
                            checked={isSelected}
                            color='secondary'
                          />
                        </TableCell>
                        {columns.map((column, id) =>
                          <TableCell
                            className={classes.tableCell}
                            key={id}>{data[column.field]}
                          </TableCell>
                        )}
                        {selected.includes(data.id) ?
                          <TableCell
                            className={classes.tableCell}>
                            <IconButton onClick={this.handleClickOpenDialog}>
                              <Edit />
                            </IconButton>
                          </TableCell> :
                          <TableCell />
                        }
                      </TableRow>
                    );
                  })
                }
                {emptyRows > 0 && (
                  <TableRow
                    style={{ height: 35 * emptyRows }}>
                    <TableCell colSpan={6} align="center"
                      className={classes.text}
                    >
                      {data.length === 0 && translate('NoRecordsToDisplay')}
                    </TableCell>
                  </TableRow>
                )}
              </TableBody>
            </Table>
          </div>
          <TablePagination
            labelRowsPerPage={translate('NumberOfRows')}
            rowsPerPageOptions={[5, 10, 25]}
            component="div"
            colSpan={3}
            count={data.length}
            rowsPerPage={rowsPerPage}
            page={page}
            onChangePage={this.handleChangePage}
            onChangeRowsPerPage={this.handleChangeRowsPerPage}
          />
        </Paper>
        <Modal
          open={openDialog}
          handleClose={this.handleCloseDialog}
        >
          {children}
        </Modal>
      </MuiThemeProvider>
    );
  }
}

export default withStyles(Style)(TableComponent);