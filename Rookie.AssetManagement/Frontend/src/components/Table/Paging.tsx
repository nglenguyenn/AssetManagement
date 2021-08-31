import React, { useMemo } from 'react';
import { ThreeDots } from 'react-bootstrap-icons';
import { NEAR_BY_PAGE_RENDER_LIMIT, DOTS } from 'src/constants/tablePaginationOption';
import { range } from 'src/utils/helper'
import { usePagination } from 'src/hooks/usePagination';

export type PageType = {
    currentPage?: number;
    totalPage?: number;
    handleChange: (page: number) => void;
}

const Paging: React.FC<PageType> = ({ currentPage = 1, totalPage = 1, handleChange }) => {
    const prePageStyle = currentPage !== 1 ? 'pagination__link' : 'pagination__link link-disable';
    const nextPageStyle = currentPage + 1 <= totalPage ? 'pagination__link' : 'pagination__link link-disable';

    const pagination = usePagination({
        currentPage,
        totalPage,
        siblingCount: NEAR_BY_PAGE_RENDER_LIMIT,
    });

    const pageStyle = (page: number) => {
        if (page === currentPage) return 'pagination__link link-active';
        return 'pagination__link';
    };

    const onPrev = (e) => {
        e.preventDefault();

        if (currentPage !== 1) {
            handleChange(currentPage - 1);
        }
    };

    const onNext = (e) => {
        e.preventDefault();

        if (currentPage + 1 <= totalPage) {
            handleChange(currentPage + 1);
        }
    };

    const onPageNumber = (e, page: number) => {
        e.preventDefault();
        handleChange(page);
    };

    return (
        <div className="w-100 d-flex align-items-center mt-3">
            <ul className="pagination">
                <li className="intro-x">
                    <a onClick={onPrev} className={prePageStyle}>
                        Previous
                    </a>
                </li>

                {
                    pagination?.map(pageNumber => {
                        if (pageNumber === DOTS) {
                            return (
                                <li className="intro-x p-2">
                                    <ThreeDots className="intro-x " />
                                </li>
                            )
                        }

                        return (
                            <li key={pageNumber} className="intro-x">
                                <a onClick={e => onPageNumber(e, pageNumber)} className={pageStyle(pageNumber)}
                                >
                                    {pageNumber}
                                </a>
                            </li>)
                    })
                }

                <li className="intro-x">
                    <a onClick={onNext} className={nextPageStyle}>Next</a>
                </li>

            </ul>
        </div>
    );
};

export default Paging;

