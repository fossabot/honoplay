import React from 'react';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles';
import {Switch, IconButton, Paper,TableRow, TableHead, 
        TableCell, TableBody, Table, Tooltip, TableFooter } from '@material-ui/core';
import TablePagination from '@material-ui/core/TablePagination';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';  
import Style from './Style';


class TableComponent extends React.Component {

  constructor(props) {
    super(props);

    this.state = {
     page: 0,
     rowsPerPage: 5,
   };
   this.handleChangePage=this.handleChangePage.bind(this);
   this.handleChangeRowsPerPage=this.handleChangeRowsPerPage.bind(this);
  }

  handleChangePage (event, page) {
  this.setState({ page });
  };

  handleChangeRowsPerPage (event) {
  this.setState({ page: 0, rowsPerPage: event.target.value });
  };

  render() {
    const { classes, columns, data, SwitchColumn, RowNumber } = this.props;
    const { rowsPerPage, page } = this.state;
    const emptyRows = rowsPerPage - Math.min(rowsPerPage, data.length - page * rowsPerPage);

    return (
      <Paper className={classes.root}>       
        <Table className={classes.table}>     
          <TableHead>
            <TableRow>
              {(RowNumber ?
                <TableCell className={classes.TableHead}>#</TableCell> : null
              )} 
              {columns.map((column,i) =>(
                <TableCell className={classes.TableHead} key={i}>{column}</TableCell>
              ))}
              {(SwitchColumn ? 
                  <TableCell className={classes.TableHead}>Durum (Pasif/Aktif)</TableCell> : null
              )}      
              <TableCell className={classes.TableHead}>İşlem</TableCell>
            </TableRow>    
          </TableHead>      
          <TableBody>        
          {data.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage).map((row,i) => (
                <TableRow className={classes.row} key={i}>
                  {(RowNumber ? 
                    <TableCell className={classes.tableCell}>{i+1}</TableCell> : null
                  )}
                  {columns.map((column,i) =>
                    <TableCell className={classes.tableCell} key={i}>{row[column]}</TableCell>
                  )}
                  {(SwitchColumn ? 
                      <TableCell className={classes.tableCell}>
                      <Switch
                        checked
                        value="checkedDurum"
                        classes={{
                          switchBase: classes.colorSwitchBase,
                          checked: classes.colorChecked,
                          bar: classes.colorBar,
                        }}
                      /> 
                      </TableCell> : null
                  )}
                <TableCell className={classes.tableCell}>
                  <Tooltip title="Şifre Değiştir">
                    <IconButton>
                      <FontAwesomeIcon className={classes.icon} icon="lock"></FontAwesomeIcon>
                    </IconButton>
                  </Tooltip>
                  <Tooltip title="Düzenle">
                    <IconButton>
                      <FontAwesomeIcon className={classes.icon} icon="edit"></FontAwesomeIcon>
                    </IconButton>
                  </Tooltip>
                </TableCell>
                </TableRow>
             ))}
          </TableBody>
          </Table>
          <Table>
            <TableFooter>
                  <TableRow >
                    <TablePagination 
                      labelRowsPerPage='Sayfa başına satır sayısı:'
                      rowsPerPageOptions={[5, 10, 25]}
                      colSpan={3}
                      count={data.length}
                      rowsPerPage={rowsPerPage}
                      page={page}
                      SelectProps={{
                        native: true,
                      }}
                      onChangePage={this.handleChangePage}
                      onChangeRowsPerPage={this.handleChangeRowsPerPage}
                    /> 
                  </TableRow>
            </TableFooter>
          </Table>
      </Paper>
    );
  }
}

TableComponent.propTypes = {
  classes: PropTypes.object.isRequired,
};

export default withStyles(Style)(TableComponent);
